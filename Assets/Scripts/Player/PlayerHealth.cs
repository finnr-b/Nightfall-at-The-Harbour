using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth = 100f;
    public float currentHealth;

    public Image healthBarFill;

    void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthUI(); // Initialize health bar
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth); // This makes sure the health doesn't go below 0 or above max

        Debug.Log("Player took " + amount + " damage. Current Health: " + currentHealth);
        UpdateHealthUI();

        if (currentHealth <= 0f)
        {
            Die();
        }
    }

    void UpdateHealthUI()
    {
        if (healthBarFill != null)
        {
            healthBarFill.fillAmount = currentHealth / maxHealth;
        }
    }

    // Take the L
    void Die()
    {
        Debug.Log("You died :(");
        SceneManager.LoadScene("GameOver");
    }
}