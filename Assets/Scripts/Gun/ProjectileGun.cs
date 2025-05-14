using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;
using JetBrains.Annotations;

public class ProjectileGun : MonoBehaviour
{
    public GameObject bullet;
    public float shootForce;

    // Some Gun Stats
    public float timeBetweenShooting, spread, reloadTime, timeBetweenShots;
    public int magazineSize, bulletsPerTap;
    public bool allowButtonHold;

    int bulletsLeft, bulletsShot;

    // Recoil
    public Rigidbody playerRb;
    public float recoilForce;

    // Some more bools
    bool shooting, readyToShoot, reloading;

    // References
    public Camera fpsCam;
    public Transform attackPoint;

    //Graphics
    public GameObject muzzleFlash;
    public TextMeshProUGUI ammunitionDisplay;

    // Some bug fixing
    public bool allowInvoke = true;
    public float muzzleFlashDuration = 0.1f;

    private void Awake()
    {
        // This will make sure the magazine is full
        bulletsLeft = magazineSize;
        readyToShoot = true;
    }
    // Update is called once per frame
    private void Update()
    {
        MyInput();

        // Set the ammo display, if it exists
        if (ammunitionDisplay != null)
            ammunitionDisplay.SetText(bulletsLeft / bulletsPerTap + " / " + magazineSize / bulletsPerTap);
    }
    private void MyInput()
    {
        // This checks if the player is allowed to hold down the button and take the input
        if (allowButtonHold)
            shooting = Input.GetMouseButton(0);
        else
            shooting = Input.GetMouseButtonDown(0);

        // The player reloads
        if (Input.GetKeyDown(KeyCode.R) && bulletsLeft < magazineSize && !reloading) Reload();
        if (readyToShoot && shooting && !reloading && bulletsLeft <= 0) Reload();

        // The player shoots
        if (readyToShoot && shooting && !reloading && bulletsLeft > 0)
            if (!PauseMenu.isPaused)
        {
            // This sets the bullets shot to 0
            bulletsShot = 0;

            Shoot();
        }
    }

    private void Shoot()
    {
        readyToShoot = false;

        // This part will find the exact hit position of my bullet using a raycast
        Ray ray = fpsCam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;

        //This will check if the ray has hit something
        Vector3 targetPoint;
        if (Physics.Raycast(ray, out hit))
            targetPoint = hit.point;
        else
            targetPoint = ray.GetPoint(75); // This is just a point far away from the player

        // This part will calculate the direction from attackPoint to targetPoint
        Vector3 directionWithoutSpread = targetPoint - attackPoint.position;

        // Caluclating the spread
        float x = Random.Range(-spread, spread);
        float y = Random.Range(-spread, spread);

        // Calculating a new direction with spread
        Vector3 directionWithSpread = directionWithoutSpread + new Vector3(x, y, 0);

        //Instantiating the bullet whilst also making sure the bullet faces the shooting direction
        GameObject currentBullet = Instantiate(bullet, attackPoint.position, Quaternion.LookRotation(directionWithSpread.normalized)); // The bullets are stored here

        // Adding force to the bullet and ensuring the bullet has a rigidbody
        Rigidbody bulletRb = currentBullet.GetComponent<Rigidbody>();

        if (bulletRb != null)
        {
            bulletRb.AddForce(directionWithSpread.normalized * shootForce, ForceMode.Impulse);
        }

        else
        {
            Debug.Log("The bullet is missing a rigidbody :|");
        }

        // This part does three things: it instantiates the muzzle flash, parents it to the attackPoint at the barrel of the pistol, and ensures it doesn't repeat by setting a muzzleFlashDuration and destorying it when the duration is over.
        if (muzzleFlash != null)
        {
            GameObject flashInstance = Instantiate(muzzleFlash, attackPoint.position, attackPoint.rotation);
            flashInstance.transform.parent = attackPoint;
            Destroy(flashInstance, muzzleFlashDuration);
        }

        // Updating the ammo count
        bulletsLeft--;
        bulletsShot++;

        // Invoking the resetShot function (if it's not already been invoked)
        if (allowInvoke)
        {
            Invoke("ResetShot", timeBetweenShooting);
            allowInvoke = false;

            // This part adds recoil from the gun to the player
            playerRb.AddForce(-directionWithSpread.normalized * recoilForce, ForceMode.Impulse);
        }

        // If there's been more than one bulletsPerTap, this bit of code will make sure to repeat the shoot function
        if (bulletsShot < bulletsPerTap && bulletsLeft > 0)
            Invoke("Shoot", timeBetweenShots);
    }
    private void ResetShot()
    {
        readyToShoot = true;
        allowInvoke = true;
    }


    private void Reload()
    {
        reloading = true;
        Invoke("ReloadFinished", reloadTime);
        Debug.Log("The player is reloading, be patient..");
    }

    private void ReloadFinished()
    {
        bulletsLeft = magazineSize;
        reloading = false;
        Debug.Log("The player has finished reloading :D");
    }
}