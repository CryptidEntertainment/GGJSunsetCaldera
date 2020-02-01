using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Peng {
    public class Player : MonoBehaviour {
        public float movementSpeed = 5f;
        public float rotationSpeed = 1f;
        public float pitchSpeed = 1f;
        public float jumpSpeed = 2f;

        private bool lockCursor = true;
        private bool mlMode = true;

        private Camera mainCamera;
        private Quaternion rotation;

        void Start() {
            mainCamera = GetComponentInChildren<Camera>();
            rotation = transform.rotation;
        }

        void Update() {
            // Gather input
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");
            float mouseHorizontal = Input.GetAxis("Mouse X");
            float mouseVertical = Input.GetAxis("Mouse Y");
            bool jump = Input.GetButtonDown("Jump");

            horizontal = ((horizontal > 0) ? 1 : ((horizontal < 0) ? -1 : 0));
            vertical = ((vertical > 0) ? 1 : ((vertical < 0) ? -1 : 0));

            PlayerMovement(horizontal, vertical, jump);

            //Updates every frame. If the player's in mouselook mode, it runs the function that does the work and checks to see if they want to lock/unlock the mouse.
            if (mlMode) {
                if (lockCursor) CameraLook(mouseHorizontal, mouseVertical);
                UpdateCursorLock();
            }
        }

        private void PlayerMovement(float horizontal, float vertical, bool jump) {
            // Movement
            if (Mathf.Sqrt(horizontal * horizontal + vertical * vertical) > 0) {
                // trigonometry functions return radians but transform stuff uses degrees
                float movementAngle = Mathf.Atan2(-vertical, horizontal) * Mathf.Rad2Deg + rotation.eulerAngles.y;
                Vector3 position = transform.position;
                position = position + Quaternion.Euler(0f, movementAngle, 0f) * Vector3.right * movementSpeed * Time.deltaTime;
                transform.position = position;
            }

            if (jump) {
                Rigidbody rb = GetComponent<Rigidbody>();
                rb.AddForce(Vector3.up * jumpSpeed);
            }

        }

        #region Scott's camera code
        private void CameraLook(float horizontal, float vertical) {
            // Look side to side
            rotation = rotation * Quaternion.Euler(0f, horizontal * rotationSpeed, 0f);
            transform.rotation = rotation;

            // Look up and down
            mainCamera.GetComponent<Transform>().localRotation *= Quaternion.Euler(-vertical * pitchSpeed, 0f, 0f);
        }

        private void SetCursorLock(bool value) {
            //this is called internally to do the locking and unlocking when you want to lock or unlock the cursor.
            lockCursor = value;
            if (lockCursor) {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            } else {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
        }

        public void SetMLMode(bool value) {
            //This is an external function to be called by other scripts to change into and out of mouselook mode entirely.
            mlMode = value;
            SetCursorLock(mlMode);
        }

        private void UpdateCursorLock() {
            //this checks every update if the player hit escape to unlock the mouse, or clicked back in.
            if (Input.GetButtonUp("Pause")) {
                SetCursorLock(false);
            } else if (Input.GetButtonDown("Fire1")) {
                SetCursorLock(true);
            }
        }
        #endregion
    }
}