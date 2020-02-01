using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Peng {
    public class Projectile : MonoBehaviour {
        public float speed = 1f;
        public Vector3 direction = Vector3.zero;
        
        void Update() {
            transform.position += direction * speed * Time.deltaTime;
        }
    }
}