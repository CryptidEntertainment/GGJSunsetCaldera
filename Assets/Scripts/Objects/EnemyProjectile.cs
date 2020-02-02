using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Scott;

namespace Peng {
    public class EnemyProjectile: InteractiveObject {
        public const float PLAYER_DETECT_RADIUS = 32f;

        public float throwStrength = 1600f;
        public float throwError = 1f;
        public GameObject projectilePrefab;
        public Transform leftHand;
        public Transform rightHand;

        public float moveSpeed = 1f;
        public float hoverRange = 1f;
        public float hoverAmplitude = 1f;
        public float hoverPeriod = 1f;

        private float nextAction;
        private float time = 0;
        private Vector3 targetLocation;
        private bool active = true;

        void Start() {
            ScheduleNextAction();
            time = Random.Range(0f, 6.28f);
            targetLocation = transform.position;
            active = true;
        }

        void Update() {
            if (!active) {
                return;
            }
            
            if (DistanceToPlayer() < PLAYER_DETECT_RADIUS) {
                transform.rotation = Quaternion.LookRotation(Player.Me.transform.position - transform.position);
            }

            transform.position = Vector3.MoveTowards(transform.position, targetLocation, moveSpeed * Time.deltaTime);
            time = time + Time.deltaTime;

            if (Time.time >= nextAction) {
                ScheduleNextAction();

                if (projectilePrefab && DistanceToPlayer() < PLAYER_DETECT_RADIUS) {
                    Vector3 handPosition = (Random.Range(0f, 1f) > 0.5f) ? leftHand.position : rightHand.position;
                    Vector3 offset = new Vector3(Random.Range(-throwError, throwError), Random.Range(-throwError, throwError), Random.Range(-throwError, throwError));
                    Projectile projectile = Instantiate(projectilePrefab, handPosition, Quaternion.identity).GetComponent<Projectile>();
                    projectile.Shoot(Vector3.Normalize((Player.Me.transform.position + Player.Me.transform.localScale.y * 0.75f * Vector3.up) - handPosition + offset) * throwStrength);
                }
            }
        }

        protected override void Disable() {
            active = false;
        }

        void OnCollisionStay(Collision c) {
            if (!active) {
                Die();
            }
        }

        private void Hover() {
            transform.position += Vector3.up * hoverAmplitude * Mathf.Sin(time * hoverPeriod) * Time.deltaTime;
        }

        private void ScheduleNextAction() {
            // magic, do not touch
            nextAction = Time.time + Random.Range(1.5f, 3.5f);
        }

        /// <summary>
        /// Methods required by IMortal
        /// </summary>

        public override void Die() {
            base.Die();
            Start();
        }
    }
}