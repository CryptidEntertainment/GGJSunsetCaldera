using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Peng {
    public enum CollisionMasks {
        DEFAULT                 = 0x0001,
        TRANSPARENT_FX          = 0x0002,
        IGNORE_RAYCAST          = 0x0004,
        THREE                   = 0x0008,
        WATER                   = 0x0010,
        UI                      = 0x0020,
        SIX                     = 0x0040,
        SEVEN                   = 0x0080,
        TERRAIN                 = 0x0100,
        PROJECTILE              = 0x0200,
        PLAYER                  = 0x0400,
        GUN                     = 0x0800,
        DEATH                   = 0x1000,
        MUSIC                   = 0x2000,
        PICKUP                  = 0x4000,
    }

    public enum GameStates {
        TITLE, PLAY, PAUSE, ABOUT
    }

    public class Player : MonoBehaviour, IMortal {
        // I'm allowed just one singleton, okay?
        public static Player Me {
            get; private set;
        } = null;

        const bool JUMP_INFINITE = false;

        public float movementSpeed = 5f;
        public float rotationSpeed = 1f;
        public float pitchSpeed = 1f;
        public float jumpSpeed = 2f;
        public float runModifier = 2f;
        public int maxHealth = 3;

        public AudioClip victoryMusic;
        public AudioSource victorySource;
        public GameObject winScreen;

        public GameObject titleStuff;
        public GameObject gameplayStuff;
        public GameObject pauseStuff;
        public GameObject aboutStuff;

        public int Health {
            get; private set;
        }

        private GameStates _mode = GameStates.TITLE;
        public GameStates Mode {
            get {
                return _mode;
            }
            private set {
                _mode = value;
                switch (value) {
                    case GameStates.TITLE:
                        SetCursorLock(false);
                        titleStuff.SetActive(true);
                        gameplayStuff.SetActive(false);
                        pauseStuff.SetActive(false);
                        aboutStuff.SetActive(false);
                        break;
                    case GameStates.PLAY:
                        SetCursorLock(true);
                        titleStuff.SetActive(false);
                        gameplayStuff.SetActive(true);
                        pauseStuff.SetActive(false);
                        aboutStuff.SetActive(false);
                        break;
                    case GameStates.PAUSE:
                        SetCursorLock(false);
                        titleStuff.SetActive(false);
                        gameplayStuff.SetActive(false);
                        pauseStuff.SetActive(true);
                        aboutStuff.SetActive(false);
                        break;
                    case GameStates.ABOUT:
                        SetCursorLock(false);
                        titleStuff.SetActive(false);
                        gameplayStuff.SetActive(false);
                        pauseStuff.SetActive(false);
                        aboutStuff.SetActive(true);
                        break;
                }
            }
        }

        private int maxJumpCount = 2;
        private int jumpsRemaining;
        private float maxIFrames = 1.5f;
        
        private Camera mainCamera;
        private Vector3 originalPosition;
        private Quaternion originalRotation;
        private Quaternion rotation;

        public float IFrames {
            get; private set;
        } = 0;

        void Awake() {
            if (Me) {
                Destroy(gameObject);
                return;
            }

            Me = this;
            Health = maxHealth;

            mainCamera = GetComponentInChildren<Camera>();
            rotation = transform.rotation;
            jumpsRemaining = maxJumpCount;
            originalPosition = transform.position;
            originalRotation = transform.rotation;
        }

        void FixedUpdate() {
            switch (Mode) {
                case GameStates.PLAY:
                    FixedUpdateGameplay();
                    break;
                default:
                    break;
            }
        }

        void Update() {
            switch (Mode) {
                case GameStates.PLAY:
                    UpdateGameplay();
                    break;
                case GameStates.PAUSE:
                case GameStates.ABOUT:
                    UpdatePause();
                    break;
                default:
                    break;
            }
        }

        public void EnterWinScreen() {
            Mode = GameStates.TITLE;
            SetCursorLock(false);
            winScreen.GetComponent<WinScreen>().Activate();

            AudioSource[] musics = GetComponentsInChildren<AudioSource>();
            foreach (AudioSource ms in musics) {
                ms.gameObject.SetActive(false);
            }

            victorySource.gameObject.SetActive(true);
            victorySource.Stop();
            victorySource.clip = victoryMusic;
            victorySource.Play();
            victorySource.volume = 0.65f;
        }

        public void EnterPlayMode() {
            Mode = GameStates.PLAY;
        }

        public void EnterPauseMode() {
            Mode = GameStates.PAUSE;
        }

        public void EnterAboutMode() {
            Mode = GameStates.ABOUT;
        }

        public void EnterQuitMode() {
            Application.Quit();
        }

        private void FixedUpdateGameplay() {
            // Gather input
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");
            float run = Input.GetAxis("Run");

            horizontal = ((horizontal > 0) ? 1 : ((horizontal < 0) ? -1 : 0));
            vertical = ((vertical > 0) ? 1 : ((vertical < 0) ? -1 : 0));
            run = (run > 0) ? runModifier : 1f;

            PlayerMovement(horizontal, vertical, run);
        }

        private void UpdateGameplay() {
            float mouseHorizontal = Input.GetAxis("Mouse X");
            float mouseVertical = Input.GetAxis("Mouse Y");
            bool jump = Input.GetButtonDown("Jump");

            CheckPause();
            CameraLook(mouseHorizontal, mouseVertical);

            if (jump && (JumpAvailable() || JUMP_INFINITE || jumpsRemaining > 0)) {
                Jump();
            }

            IFrames = Mathf.Max(0f, IFrames - Time.deltaTime);

            transform.position = GetComponentInChildren<Rigidbody>().transform.position;
        }

        private void UpdatePause() {
            CheckUnpause();
        }

        private void PlayerMovement(float horizontal, float vertical, float speedModifier) {
            // Movement
            if (Mathf.Sqrt(horizontal * horizontal + vertical * vertical) > 0) {
                // trigonometry functions return radians but transform stuff uses degrees
                float movementAngle = Mathf.Atan2(-vertical, horizontal) * Mathf.Rad2Deg + rotation.eulerAngles.y;
                Vector3 position = transform.position;
                position = position + Quaternion.Euler(0f, movementAngle, 0f) * Vector3.right * movementSpeed * speedModifier * Time.deltaTime;
                transform.position = position;
            }
        }

        public void Jump() {
            Rigidbody rb = GetComponentInChildren<Rigidbody>();
            rb.AddForce(Vector3.up * jumpSpeed);
            jumpsRemaining--;
        }

        public bool JumpAvailable() {
            CapsuleCollider collider = GetComponentInChildren<CapsuleCollider>();
            Vector3 halfHeight = Vector3.up * (collider.height - collider.radius);
            float checkDistance = 3f;
            float checkRadius = 0.8f;
            bool floored = Physics.CapsuleCast(transform.position - halfHeight, transform.position - halfHeight, collider.radius * checkRadius, Vector3.down, checkDistance, ((int)CollisionMasks.TERRAIN));
            if (floored) {
                jumpsRemaining = maxJumpCount;
            }
            return floored;
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
            Cursor.lockState = value ? CursorLockMode.Locked : CursorLockMode.None;
            Cursor.visible = !value;
        }
        #endregion

        private void CheckPause() {
            //this checks every update if the player hit escape to unlock the mouse, or clicked back in.
            if (Input.GetButtonDown("Pause")) {
                Mode = GameStates.PAUSE;
            }
        }

        private void CheckUnpause() {
            if (Input.GetButtonDown("Pause")) {
                Mode = GameStates.PLAY;
            }
        }

        public void Damage(int amount = 1, float iFrames = -1f) {
            if (IFrames > 0) {
                return;
            }

            if (iFrames > 0) {
                IFrames = iFrames;
            } else {
                IFrames = maxIFrames;
            }

            Health = Mathf.Min(Health - amount, maxHealth);
            
            if (Health <= 0) {
                Die();
            }
        }

        /// <summary>
        /// Methods required by IMortal
        /// </summary>
        public void Die() {
            transform.position = originalPosition;
            transform.rotation = originalRotation;
            Health = maxHealth;
        }
    }
}