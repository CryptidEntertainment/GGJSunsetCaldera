using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Scott;

namespace Peng {
    public class EnemyRoomba: InteractiveObject {
        public List<Transform> waypoints = new List<Transform>();
        public float movementSpeed = 1f;
        public float waypointDelay = 0.25f;

        private int waypointIndex = 0;
        private float waypointCooldown = 0;

        void Start() {
        }

        void Update() {
            SeekWaypoint();
        }

        private void ScheduleNextAction() {
        }

        private void SeekWaypoint() {
            if (waypoints.Count < 2) {
                return;
            }

            if (waypointCooldown > 0) {
                waypointCooldown = waypointCooldown - Time.deltaTime;
                return;
            }

            if (Vector3.Distance(waypoints[waypointIndex].position, transform.position) < movementSpeed * Time.deltaTime) {
                transform.position = waypoints[waypointIndex].position;
                waypointIndex = ++waypointIndex % waypoints.Count;
                waypointCooldown = waypointDelay;
            } else {
                transform.position = Vector3.MoveTowards(transform.position, waypoints[waypointIndex].position, movementSpeed * Time.deltaTime);
            }
        }
    }
}