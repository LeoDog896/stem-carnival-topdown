using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class EnemyHealthTracking : MonoBehaviour
{
    [SerializeField] private GameObject destroyTarget;

    public int maxHealth = 3;
    public int currentHealth;

    private bool isCooldown = false;
    private float cooldownDuration = 0.1f;
    private float cooldownTimer = 0.0f;

    public float knockbackForce = 5f; // Adjust the knockback force magnitude

    void Start()
    {
        currentHealth = maxHealth;
    }

    void Update()
    {
        if (isCooldown)
        {
            cooldownTimer -= Time.deltaTime;
            if (cooldownTimer <= 0.0f)
            {
                isCooldown = false; // Cooldown is over
            }
        }
    }

    public void TakeDamage(int damageAmount, Vector2 hitDirection)
    {
        currentHealth -= damageAmount;

        if (currentHealth <= 0)
        {
            Die();
        }
        else
        {
            ApplyKnockback(hitDirection.normalized);
        }
    }

    private void ApplyKnockback(Vector2 hitDirection)
    {
        // Normalize the hit direction vector to ensure consistent knockback force
        hitDirection.Normalize();

        // Apply the knockback force to the enemy's Rigidbody2D
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = Vector2.zero; // Reset velocity to avoid accumulation
            rb.AddForce(-hitDirection * knockbackForce, ForceMode2D.Impulse);
        }
    }

    void Die()
    {
        // Add game over logic here, such as displaying a game over screen, resetting the level, etc.
        if (gameObject.CompareTag("Player"))
        {
            Debug.Log("Player died!");
        } else { 
            if (destroyTarget != null)
            {
                Destroy(destroyTarget);
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !isCooldown)
        {
            Health healthC = collision.gameObject.GetComponent<Health>();
            healthC.TakeDamage(1);
            StartCooldown();
        }
    }

    private void StartCooldown()
    {
        isCooldown = true;
        cooldownTimer = cooldownDuration;
    }
}
