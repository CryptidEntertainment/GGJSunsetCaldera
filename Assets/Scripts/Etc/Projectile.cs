using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Peng {
    public class Projectile : MonoBehaviour {
        public float speed = 1f;
        public float lifespan = 10f;
        public Vector3 direction = Vector3.zero;

        private float created;

        void Update() {
            transform.position += direction * speed * Time.deltaTime;
            lifespan = lifespan - Time.deltaTime;

            if (lifespan <= 0) {
                Destroy(gameObject);
            }
        }

        void OnTriggerEnter(Collider c) {
        }
    }
}