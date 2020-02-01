using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Scott;

namespace Peng {
    public class EnemyScuttle : InteractiveObject {
        public float moveSpeed = 2f;

        private float nextAction;
        private Vector3 targetLocation;

        void Start() {
            ScheduleNextAction();
        }

        void Update() {
            if (Time.time >= nextAction) {
                ScheduleNextAction();
            }

            transform.position = Vector3.MoveTowards(transform.position, targetLocation, moveSpeed * Time.deltaTime);
        }

        private void ScheduleNextAction() {
            // find the next place you want to move to
            float moveDistance = Random.Range(1f, 2f);
            float moveAngle = Random.Range(0f, 360f);
            targetLocation = transform.position + Quaternion.Euler(0f, moveAngle, 0f) * Vector3.forward * moveDistance;
            // magic, do not touch
            nextAction = Time.time + Random.Range(1.5f, 3.5f);
        }
    }
}