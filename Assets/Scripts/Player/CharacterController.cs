using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private GameObject hitbox;
    private bool canMove = false;

    private void Start()
    {
        hitbox.SetActive(false); // Ensure hitbox is initially disabled
    }

    private void Update()
    {
        if (canMove)
        {
            Move();
            RotateTowardsMouse();
            HandleHitboxActivation();
        }
    }

    private void Move()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector2 movementDirection = new Vector2(horizontalInput, verticalInput).normalized;
        transform.Translate(movementDirection * speed * Time.deltaTime, Space.World);
    }

    private void RotateTowardsMouse()
    {
        Vector2 mouseScreenPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = -(mouseScreenPosition - (Vector2)transform.position).normalized;
        transform.up = direction;
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