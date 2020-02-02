using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Peng {
    public class HealingVolume : MonoBehaviour {
        void OnTriggerStay(Collider c) {
            Player player = c.GetComponent<Player>();
            if (player) {
                player.Damage(-1, 0.5f);
            }
        }
    }
}