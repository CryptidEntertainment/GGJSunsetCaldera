using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Scott;

namespace Peng {
    public class EnemyFrogyOfEvil: InteractiveObject {
        public List<Transform> waypoints = new List<Transform>();
        public float movementSpeed = 1f;
        public float waypointDelay = 1f;

        private int waypointIndex = 0;
        private float waypointCooldown = 0;
        private FrogyStates state = FrogyStates.IDLE;

        private enum FrogyStates {
            IDLE, PURSUE, ATTACK, RETREAT,
        }

        void Awake() {
            throw new System.Exception("This has not been implemented, please do not use");
        }

        void Update() {
            switch (state) {
                case FrogyStates.IDLE:
                    SeekWaypoint();
                    break;
                case FrogyStates.PURSUE:
                    break;
                case FrogyStates.RETREAT:
                    break;
                case FrogyStates.ATTACK:
                    break;
            }
        }

        void OnCollisionStay(Collision c) {
            Player player = c.gameObject.GetComponent<Player>();
            if (player) {
                player.Damage();
            }
        }

        private float DistanceToPlayer() {
            return Vector3.Distance(Player.Me.transform.position, transform.position);
        }

        private void SeekWaypoint() {
            if (waypoints.Count < 1) {
                return;
            }

            if (waypointCooldown > 0) {
                waypointCooldown = waypointCooldown - Time.deltaTime;
                return;
            }

            if (Vector3.Distance(waypoints[waypointIndex].position, transform.position) < movementSpeed * Time.deltaTime) {
                transform.position = waypoints[waypointIndex].position;
                waypointIndex = ((int)Random.Range(0f, waypoints.Count - 1));
                waypointCooldown = waypointDelay;
                state = FrogyStates.IDLE;
            } else {
                transform.position = Vector3.MoveTowards(transform.position, waypoints[waypointIndex].position, movementSpeed * Time.deltaTime);
            }
        }
    }
}