using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDamage : MonoBehaviour
{
    public float damage = 25f;
    public float lifespan = 5f;

    // Since both the player and enemy are firing a gun, this will help keep track of who fired this bullet
    private GameObject shooter;

    public void SetShooter(GameObject shooterRef)
    {
        shooter = shooterRef;
    }

    void Awake()
    {
        Destroy(gameObject, lifespan);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (shooter != null && collision.transform.root.gameObject == shooter.transform.root.gameObject)
        {
            return;
        }

        // Checking for damage for the player and enemy
        PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();
        if (playerHealth != null)
        {
            Debug.Log("Bullet hit Player for " + damage + " damage, don't die!");
            playerHealth.TakeDamage(damage);
            DestroyBullet();
            return;
        }

        void DestroyBullet()
        {
            Destroy(gameObject);
        }

        Target target = collision.gameObject.GetComponent<Target>();
        if (target != null)
        {
            Debug.Log("Bullet hit Target " + target.name + " for " + damage + " damage, keep going!");
            target.TakeDamage(damage);
            DestroyBullet();
            return;
        }
    }
}