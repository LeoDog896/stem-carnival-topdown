using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Health : MonoBehaviour
{
    public int maxHealth = 3; // Maximum health of the player
    public int currentHealth;   // Current health of the player

    private bool isCooldown = false; // Flag to indicate if the player is on cooldown
    private float cooldownDuration = 1.0f; // Cooldown duration in seconds
    private float cooldownTimer = 0.0f; // Timer to track cooldown progress

    public Text healthText;


    void Start()
    {
        UpdateHealthText();
        Time.timeScale = 1f;
        currentHealth = maxHealth; // Initialize current health to maximum health
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

    // Method to take damage
    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;

        if (currentHealth <= 0)
        {
            // Player is dead, handle death logic here (e.g., game over screen)
            Die();
        }
        UpdateHealthText();
    }

    // Method to heal
    public void Heal(int healAmount)
    {
        currentHealth = Mathf.Min(currentHealth + healAmount, maxHealth);
        UpdateHealthText();
    }

    // Method to handle player death
    void Die()
    {
        Time.timeScale = 0f;
        // Add game over logic here, such as displaying a game over screen, resetting the level, etc.
        Debug.Log("Player died!");
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if colliding with an enemy
        if (collision.gameObject.CompareTag("Enemy") && !isCooldown)
        {
            // Reduce damage by 1
            TakeDamage(1);
            // Start cooldown
            StartCooldown();
        }
    }
    private void StartCooldown()
    {
        isCooldown = true;
        cooldownTimer = cooldownDuration;
    }

    void UpdateHealthText()
    {
        healthText.text = "Health: " + currentHealth.ToString();
    }

}

