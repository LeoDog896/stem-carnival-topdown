using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    [SerializeField]
    private float speed;

    private bool canMove = false;

    [SerializeField]
    private float rotationSpeed;

    private Rigidbody2D _rigidbody;

    private Camera _camera;

    [SerializeField] Transform hand;

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _camera = Camera.main;
    }

    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector2 movementDirection = new Vector2(horizontalInput, verticalInput);
        float inputMagnitude = Mathf.Clamp01(movementDirection.magnitude);

        if (canMove)
        {
            movementDirection.Normalize();
            transform.Translate(movementDirection * speed * inputMagnitude * Time.deltaTime, Space.World);
        }

        Vector2 mouseScreenPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = -(mouseScreenPosition - (Vector2)transform.position).normalized;

        transform.up = direction;

    }

// Method to enable or disable player movement
public void EnableMovement(bool enable)
    {
        canMove = enable;
    }
}
