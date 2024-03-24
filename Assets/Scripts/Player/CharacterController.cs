using System.Collections;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private GameObject hitbox;
    private bool canMove = false;

    public Rigidbody2D rigidBody;

    Vector2 movement;

    private void Start()
    {
        hitbox.SetActive(false); // Ensure hitbox is initially disabled
    }

    private void Update()
    {
        if (canMove)
        {
            movement.x = Input.GetAxisRaw("Horizontal");
            movement.y = Input.GetAxisRaw("Vertical");
            RotateTowardsMouse();
            HandleHitboxActivation();
        }
    }

    private void FixedUpdate()
    {
        rigidBody.MovePosition(rigidBody.position + movement * speed * Time.fixedDeltaTime);
    }

    private void RotateTowardsMouse()
    {
        Vector2 mouseScreenPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = -(mouseScreenPosition - (Vector2)transform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotationSpeed * Time.deltaTime);
    }

    private void HandleHitboxActivation()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            StartCoroutine(ActivateHitbox());
        }
    }

    private IEnumerator ActivateHitbox()
    {
        hitbox.SetActive(true); // Enable hitbox
        yield return new WaitForSeconds(0.3f); // Wait for hitbox duration
        hitbox.SetActive(false); // Disable hitbox
        yield return new WaitForSeconds(0.2f); // Cooldown duration
    }

    // Method to enable or disable player movement
    public void EnableMovement(bool enable)
    {
        canMove = enable;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && hitbox.activeSelf)
        {
            EnemyHealthTracking hitevent = collision.gameObject.GetComponent<EnemyHealthTracking>();
            hitevent.TakeDamage(1, collision.relativeVelocity.normalized * 5);
        }
    }
}