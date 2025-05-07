using UnityEngine;

public class Target : MonoBehaviour
{
    [Header("Enemy Stats")]
    public float health = 100f;

    public void TakeDamage(float amount)
    {
        health -= amount;
        Debug.Log(gameObject.name + " took " + amount + " damage. Current Health: " + health); // Feedback
        if (health <= 0f)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log(gameObject.name + " has died L bozo");
        EnemyGunController gun = GetComponent<EnemyGunController>();
        if (gun != null) gun.enabled = false;

        Destroy(gameObject, 0.1f); // Slight delay to make sure the damage is registered
    }

}