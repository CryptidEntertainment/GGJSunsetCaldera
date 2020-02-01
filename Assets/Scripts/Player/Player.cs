using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Peng {
    enum CollisionMasks {
        DEFAULT                 = 0x0001,
        TRANSPARENT_FX          = 0x0002,
        IGNORE_RAYCAST          = 0x0004,
        THREE                   = 0x0008,
        WATER                   = 0x0010,
        UI                      = 0x0020,
        SIX                     = 0x0040,
        SEVEN                   = 0x0080,
        TERRAIN                 = 0x0100,
    }

    public class Player : MonoBehaviour {
        const bool JUMP_INFINITE = false;

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

        void FixedUpdate() {
            // Gather input
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");

            horizontal = ((horizontal > 0) ? 1 : ((horizontal < 0) ? -1 : 0));
            vertical = ((vertical > 0) ? 1 : ((vertical < 0) ? -1 : 0));

            PlayerMovement(horizontal, vertical);
        }

        void Update() {
            float mouseHorizontal = Input.GetAxis("Mouse X");
            float mouseVertical = Input.GetAxis("Mouse Y");
            bool jump = Input.GetButtonDown("Jump");

            if (mlMode) {
                if (lockCursor) CameraLook(mouseHorizontal, mouseVertical);
                UpdateCursorLock();
            }

            if (jump && (JumpAvailable() || JUMP_INFINITE)) {
                Jump();
            }
        }

        private void PlayerMovement(float horizontal, float vertical) {
            // Movement
            if (Mathf.Sqrt(horizontal * horizontal + vertical * vertical) > 0) {
                // trigonometry functions return radians but transform stuff uses degrees
                float movementAngle = Mathf.Atan2(-vertical, horizontal) * Mathf.Rad2Deg + rotation.eulerAngles.y;
                Vector3 position = transform.position;
                position = position + Quaternion.Euler(0f, movementAngle, 0f) * Vector3.right * movementSpeed * Time.deltaTime;
                transform.position = position;
            }
        }

        public void Jump() {
            Rigidbody rb = GetComponent<Rigidbody>();
            rb.AddForce(Vector3.up * jumpSpeed);
        }

        public bool JumpAvailable() {
            CapsuleCollider collider = GetComponent<CapsuleCollider>();
            Vector3 halfHeight = Vector3.up * (collider.height - collider.radius);
            return Physics.CapsuleCast(transform.position - halfHeight, transform.position - halfHeight, collider.radius * 0.5f, Vector3.down, 1, ((int)CollisionMasks.TERRAIN));
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