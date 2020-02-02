using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Scott;

namespace Peng {
    public class EnemyRoomba: InteractiveObject {
        // hard-coding this because it ought to be the same for every instance of roomba
        public const float PLAYER_DETECT_RADIUS = 32f;
        // you will chase the player for farther than you can see them, once you're aware of where they are
        public const float PLAYER_CHASE_RADIUS = 48f;

        public List<Transform> waypoints = new List<Transform>();
        public float movementSpeed = 1f;
        public float waypointDelay = 0.25f;
        public float maxRadius = 96f;
        public float knockbackSpeed = 8;
        public float rotationSpeed = 720f;
        public Transform startingPosition;

        public float hoverAmplitude = 1f;
        public float hoverPeriod = 1f;

        private float time = 0;
        private int waypointIndex = 0;
        private float waypointCooldown = 0;
        private RoombaStates state = RoombaStates.IDLE;

        private enum RoombaStates {
            IDLE, CHASE, RETREAT, DISABLED
        }

        void Start() {
            time = Random.Range(0f, 6.28f);
            waypointIndex = 0;
            waypointCooldown = 0;
            state = RoombaStates.IDLE;
    }

        void Update() {
            time = time + Time.deltaTime;

            // if you're too far away from the origin, no matter what you're doing, stop being too far
            // away from the origin
            if (Vector3.Distance(transform.position, startingPosition.position) >= maxRadius) {
                state = RoombaStates.RETREAT;
            }

            switch (state) {
                case RoombaStates.IDLE:
                    SeekWaypoint();
                    DetectPlayer();
                    Whirrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrr();
                    Hover();
                    break;
                case RoombaStates.CHASE:
                    ChasePlayer();
                    Whirrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrr();
                    Hover();
                    break;
                case RoombaStates.RETREAT:
                    RetreatToWaypoint();
                    SeekWaypoint();
                    Whirrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrr();
                    Hover();
                    break;
                case RoombaStates.DISABLED:
                    break;
            }
        }

        void OnCollisionStay(Collision c) {
            Player player = c.gameObject.GetComponent<Player>();
            if (player) {
                player.Damage();
                player.GetComponent<Rigidbody>().velocity = knockbackSpeed * (player.transform.position - transform.position);
            } else if (state == RoombaStates.DISABLED) {
                Die();
            }
        }

        protected override void Disable() {
            state = RoombaStates.DISABLED;
        }

        private void Hover() {
            transform.position += Vector3.up * hoverAmplitude * Mathf.Sin(time * hoverPeriod) * Time.deltaTime;
        }

        private void DetectPlayer() {
            if (DistanceToPlayer() < PLAYER_DETECT_RADIUS) {
                state = RoombaStates.CHASE;
                return;
            }
        }

        private void Whirrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrr() {
            transform.rotation *= Quaternion.Euler(0f, rotationSpeed * Time.deltaTime, 0f);
        }

        private void ChasePlayer() {
            if (DistanceToPlayer() > PLAYER_CHASE_RADIUS) {
                state = RoombaStates.RETREAT;
                return;
            }
            
            transform.position = Vector3.MoveTowards(transform.position, Player.Me.transform.position, movementSpeed * Time.deltaTime);
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
                state = RoombaStates.IDLE;
            } else {
                transform.position = Vector3.MoveTowards(transform.position, waypoints[waypointIndex].position, movementSpeed * Time.deltaTime);
            }
        }

        private void RetreatToWaypoint() {
            waypointIndex = 0;
            for (int i = 1; i < waypoints.Count; i++) {
                if (Vector3.Distance(waypoints[i].position, transform.position) < Vector3.Distance(waypoints[waypointIndex].position, transform.position)) {
                    waypointIndex = i;
                }
            }
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