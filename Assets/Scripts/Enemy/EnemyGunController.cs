using UnityEngine;

public class EnemyGunController : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePoint;

    public float bulletForce = 20f;
    public float bulletDamage = 10f;

    public void Shoot()
    {
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