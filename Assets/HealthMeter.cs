using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Peng {
    public class HealthMeter : MonoBehaviour {
        public List<Sprite> sprites;

        void Update() {
            int amount = ((int)Mathf.Floor(Player.Me.Health));
            Image image = GetComponent<Image>();
            image.sprite = sprites[amount];
        }
    }
}