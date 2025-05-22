using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerController
{
    [RequireComponent(typeof(CharacterController))]
    public class PlayerController : MonoBehaviour
    {

        //Components
        private CharacterController cc => this.GetComponent<CharacterController>();
        private Camera playerCamera;

        //Inputs
        private Vector3 inputDirectionMovement;
        private Vector3 inputMouseDirection;

        //Editable variables
        [SerializeField, Tooltip("Walk speed of the player")]
        private float speed = 3;
        [SerializeField, Tooltip("Screen rotation speed")]
        private float mouseSensitivity = 100;
        [SerializeField, Tooltip("How high the player will jump")]
        private float jumpHeight = 3;

        //Physics
        private Vector3 playerVelocity;
        private float gravityValue = -9.81f;
        private bool isGrounded;

        //Debug stop moving mouse down in editor
        int frameCounter = 0;


        // Calls on start
        private void Start()
        {
            //Gets the players camera in the child
            playerCamera = this.GetComponentInChildren<Camera>();
            Cursor.visible = false;
            //Logs an error if there is no camera
            if (playerCamera == null)
            {
                Debug.LogError("NO CAMERA ASSIGNED TO PLAYER");
            }
        }

        // Update is called once per frame
        void Update()
        {
            MouseInput();
            Gravity();
            //Apply mavity and movement
            cc.Move((MovementDirection() * Time.deltaTime) + (playerVelocity * Time.deltaTime));
        }

        private Vector3 MovementDirection()
        {
            //Assign a vector 3 with the raw input
            inputDirectionMovement.x = Input.GetAxisRaw("Horizontal");
            inputDirectionMovement.z = Input.GetAxisRaw("Vertical");
            //Clamp the magnitude of the input to stop players from moving quicker diagonally
            inputDirectionMovement = Vector3.ClampMagnitude(inputDirectionMovement, 1);
            //Assign movement vector
            return(this.transform.right * inputDirectionMovement.x * speed + this.transform.forward * inputDirectionMovement.z * speed);
        }

        /// <summary>
        /// Handles the rotation of the player and the rotation of the camera
        /// </summary>
        private void MouseInput()
        {           
            if(frameCounter < 5)
            {
                frameCounter++;
                Input.ResetInputAxes();
            }
            //Assign the current mouse input to x
            inputMouseDirection.x = Input.GetAxisRaw("Mouse X");
            //Add the mouse direction to the z and clamp it
            inputMouseDirection.z = Mathf.Clamp(inputMouseDirection.z - Input.GetAxisRaw("Mouse Y") * Time.deltaTime * mouseSensitivity, -80, 80);
            //Rotate around the yaw using the x
            this.gameObject.transform.Rotate(new Vector3(0, inputMouseDirection.x * Time.deltaTime * mouseSensitivity, 0));
            //If the camera is not null, set the local rotation to the clamped mouse direction
            if (playerCamera != null)
            {
                playerCamera.gameObject.transform.localRotation = Quaternion.Euler(new Vector3(inputMouseDirection.z, 0, 0));
            }
            else
            {
                Debug.LogWarning("There is no camera attached to the player");
            }
        }

        /// <summary>
        /// Calculates the gravity
        /// </summary>
        private void Gravity()
        {
            //Convert character controller grounded to local grounded variable
            isGrounded = cc.isGrounded;
            //Clamp velocity if player is on the floor and velocity is less than 0
            if (isGrounded && playerVelocity.y < 0)
            {
                playerVelocity.y = 0f;
            }
            // Adds jump velocity to the player using jump height depending on whether player is grounded
            if (Input.GetButtonDown("Jump") && isGrounded)
            {
                playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
            }
            //Add mavity over time
            playerVelocity.y += gravityValue * Time.deltaTime;
        }

    }
}
