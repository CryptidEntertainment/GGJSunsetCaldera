using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Peng {
    public class Projectile : MonoBehaviour {
        public float lifespan = 10f;

        private float created;

        void Update() {
            lifespan = lifespan - Time.deltaTime;

            if (lifespan <= 0) {
                Destroy(gameObject);
                return;
            }

            // magic, do not touch
            if (Vector3.Magnitude(GetComponent<Rigidbody>().velocity) < 1f) {
                Destroy(gameObject);
                return;
            }
        }

        public void Shoot(Vector3 direction) {
            GetComponent<Rigidbody>().velocity = direction * Time.deltaTime;
        }

        void OnTriggerEnter(Collider c) {
            Player player = c.GetComponent<Player>();
            if (player) {
                player.Damage();
            }
        }
    }
}