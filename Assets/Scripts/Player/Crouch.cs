using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crouch : MonoBehaviour
{
    public CharacterController PlayerController;
    public float crouchSpeed, normalHeight, crouchHeight;
    public Vector3 offset;
    public Transform player;
    bool crouching;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            crouching = !crouching;
        }

        if (crouching == true)
        {
            PlayerController.height = PlayerController.height - crouchSpeed * Time.deltaTime;
            if(PlayerController.height <= crouchHeight)
            {
                PlayerController.height = crouchHeight;
            }
        }

        if (crouching == false)
        {
            PlayerController.height = PlayerController.height + crouchSpeed * Time.deltaTime;
            if (PlayerController.height < normalHeight)
            {
                player.gameObject.SetActive(false);
                player.position = player.position + offset * Time.deltaTime;
                player.gameObject.SetActive(true);
            }

            if (PlayerController.height >= normalHeight)
            {
                PlayerController.height = normalHeight;
            }
        }
    }
}