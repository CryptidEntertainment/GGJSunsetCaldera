using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Peng {
    public class HealthMeter : MonoBehaviour {
        public List<Sprite> sprites;

        void Update() {
            GetComponent<Image>().sprite = sprites[((int)Mathf.Floor(Player.Me.Health)) - 1];
        }
    }
}