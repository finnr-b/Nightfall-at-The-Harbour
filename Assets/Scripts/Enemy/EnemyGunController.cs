using UnityEngine;

public class EnemyGunController : MonoBehaviour
{
    [Header("Gun Setup")]
    public GameObject bulletPrefab;
    public Transform firePoint;

    [Header("Bullet Properties")]
    public float bulletForce = 20f;
    public float bulletDamage = 10f;

    void Start()
    {
        if (bulletPrefab == null)
        {
            Debug.LogError("You're missing the bullet prefab for the enemy, dummy.", this);
            this.enabled = false;
        }
        if (firePoint == null)
        {
            Debug.LogError("You're missing the firing point for the enemy, stupid", this);
            this.enabled = false;
        }
    }

    public void Shoot()
    {
        if (bulletPrefab == null || firePoint == null) return; // Safety check

        GameObject enemyBullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

        BulletDamage bulletScript = enemyBullet.GetComponent<BulletDamage>();
        if (bulletScript != null)
        {
            bulletScript.SetShooter(transform.root.gameObject);
            bulletScript.damage = this.bulletDamage;
        }

        // Add force to the bullet
        Rigidbody rb = enemyBullet.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.AddForce(firePoint.forward * bulletForce, ForceMode.VelocityChange);
        }

    }

}