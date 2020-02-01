using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Scott;

namespace Peng {
    public class EnemyProjectileBadly: InteractiveObject {
        public float throwSpeed = 1f;
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
                    Projectile projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity).GetComponent<Projectile>();
                    projectile.direction = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f));
                    projectile.speed = throwSpeed;
                }
            }
        }

        private void ScheduleNextAction() {
            // magic, do not touch
            nextAction = Time.time + Random.Range(1.5f, 3.5f);
        }
    }
}