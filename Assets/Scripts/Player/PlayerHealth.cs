using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth = 100f;
    public float currentHealth;

    public TextMeshProUGUI healthTextDisplay;

    public float debugDamageAmount = 10f;
    public KeyCode debugDamageKey = KeyCode.F;

    void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthUI(); // Initialize health bar
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown(debugDamageKey))
        {
            TakeDamage(debugDamageAmount);
        }
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth); // This makes sure the health doesn't go below 0 or above max

        Debug.Log("Finn made you take damage to test something");
        UpdateHealthUI();

        if (currentHealth <= 0f)
        {
            Die();
        }
    }

    void UpdateHealthUI()
    {
        if (healthTextDisplay != null)
        {
            healthTextDisplay.text = "HP: " + Mathf.RoundToInt(currentHealth).ToString() + " / " + Mathf.RoundToInt(maxHealth).ToString();
        }
    }

    // Take the L
    void Die()
    {
        Debug.Log("You died :(");
        SceneManager.LoadScene("GameOver");
    }
}