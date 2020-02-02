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
            }
        }

        public void Shoot(Vector3 direction) {
            GetComponent<Rigidbody>().velocity = direction * Time.deltaTime;
        }

        void OnTriggerEnter(Collider c) {
        }
    }
}