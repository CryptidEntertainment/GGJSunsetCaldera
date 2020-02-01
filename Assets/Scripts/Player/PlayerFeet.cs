using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Peng {
    public class PlayerFeet : MonoBehaviour {
        private HashSet<Collider> currentlyColliding = new HashSet<Collider>();
        public int size = 0;
        public bool available = false;

        public bool JumpAvailable {
            get; private set;
        }

        void OnTriggerEnter(Collider collider) {
            if (!currentlyColliding.Contains(collider)) {
                currentlyColliding.Add(collider);
                JumpAvailable = true;
                available = true;
            }
            
            size = currentlyColliding.Count;
        }

        void OnTriggerExit(Collider collider) {
            if (currentlyColliding.Contains(collider)) {
                currentlyColliding.Remove(collider);
            }
            if (currentlyColliding.Count == 0) {
                JumpAvailable = false;
                available = false;
            }
            
            size = currentlyColliding.Count;
        }
    }
}