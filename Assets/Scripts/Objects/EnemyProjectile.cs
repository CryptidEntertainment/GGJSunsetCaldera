using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Scott;

namespace Peng {
    public class EnemyProjectile: InteractiveObject {
        public float throwStrength = 1f;
        public float throwError = 1f;
        // for best results, this should be a prefab that interacts with the physics system unnecessarily
        public GameObject projectilePrefab;

        private float nextAction;

        void Start() {
            ScheduleNextAction();
        }

        void Update() {
            if (Time.time >= nextAction) {
                ScheduleNextAction();

                if (projectilePrefab) {
                    Vector3 offset = new Vector3(Random.Range(-throwError, throwError), Random.Range(-throwError, throwError), Random.Range(-throwError, throwError));
                    Projectile projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity).GetComponent<Projectile>();
                    projectile.direction = Player.Me.transform.position - transform.position + Player.Me.transform.localScale.y * Vector3.up + offset;
                    projectile.speed = throwStrength;
                }
            }
        }

        private void ScheduleNextAction() {
            // magic, do not touch
            nextAction = Time.time + Random.Range(1.5f, 3.5f);
        }
    }
}