using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Scott;

namespace Peng {
    public class EnemyProjectile: InteractiveObject {
        public float throwStrength = 1600f;
        public float throwError = 1f;
        public GameObject projectilePrefab;
        public Transform leftHand;
        public Transform rightHand;

        private float nextAction;

        void Start() {
            ScheduleNextAction();
        }

        void Update() {
            if (Time.time >= nextAction) {
                ScheduleNextAction();

                if (projectilePrefab) {
                    Vector3 handPosition = (Random.Range(0f, 1f) > 0.5f) ? leftHand.position : rightHand.position;
                    Vector3 offset = new Vector3(Random.Range(-throwError, throwError), Random.Range(-throwError, throwError), Random.Range(-throwError, throwError));
                    Projectile projectile = Instantiate(projectilePrefab, handPosition, Quaternion.identity).GetComponent<Projectile>();
                    projectile.Shoot(Vector3.Normalize((Player.Me.transform.position + Player.Me.transform.localScale.y * 0.75f * Vector3.up) - handPosition + offset) * throwStrength);
                }
            }
        }

        private void ScheduleNextAction() {
            // magic, do not touch
            nextAction = Time.time + Random.Range(1.5f, 3.5f);
        }
    }
}