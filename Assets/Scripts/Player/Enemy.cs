using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Enemy : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f; // Adjust as needed
    [SerializeField] private float rotateSpeed = 0.75f;
    [SerializeField] private float attackRange = 0.5f; // Maximum distance at which the enemy can attack
    [SerializeField] private float prepareDuration = 0.45f; // Duration of the prepare phase
    [SerializeField] private Color prepareColor = Color.yellow; // Color during the prepare phase
    [SerializeField] private Color attackColor = Color.red; // Color during the attack phase
    [SerializeField] private Color normalColor = Color.white; // Normal color

    private float distanceToPlayer = 1.0f;

    private bool attacked = false;
    private Transform player; // Reference to the player's transform
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private bool canAttack = true;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

        distanceToPlayer = Vector2.Distance(transform.position, player.position);

        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        if (player == null)
        {
            Debug.LogError("Player GameObject not found!");
        }
    }

    void Update()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

        distanceToPlayer = Vector2.Distance(this.transform.position, player.position);

        // Calculate the distance between the enemy and the player

        // If the player is within a certain distance, initiate the attack
        if (distanceToPlayer <= attackRange && canAttack == true)
        {
            spriteRenderer.color = attackColor; // Change color to attack color
            Invoke("Cooldown", 0.1f);
        }
        else
        {
            if (!attacked)
            {
                transform.position = Vector2.MoveTowards(transform.position, player.position, Time.deltaTime * moveSpeed);
                RotateTowardsTarget();
                spriteRenderer.color = normalColor; // Change color to normal
            }
        }
    }

    private void RotateTowardsTarget()
    {
        // Calculate the direction vector from the enemy to the player
        Vector2 targetDirection = player.position - transform.position;

        // Calculate the angle to rotate towards the player
        float angle = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg - 270f;

        // Rotate the enemy towards the player
        Quaternion q = Quaternion.Euler(new Vector3(0, 0, angle));
        transform.localRotation = Quaternion.Slerp(transform.localRotation, q, rotateSpeed);
    }

    IEnumerator Cooldown() //checks for cooldown
    {
        if (attacked)
        {
            canAttack = false;

            yield return new WaitForSeconds(0.6f);

            canAttack = true;

            attacked = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player") && canAttack == true)
        {
            attacked = true;
        }
    }
}
