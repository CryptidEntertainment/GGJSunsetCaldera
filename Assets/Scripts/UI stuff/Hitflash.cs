using UnityEngine;
using UnityEngine.UI;

namespace Peng {
    public class Hitflash : MonoBehaviour {
        const float rate = 2f;
        const float max_flash = 0.5f;
        void Update() {
            Image image = GetComponent<Image>();
            if (!image) return;
            Color c = image.color;
            c.a = Mathf.Max(c.a - rate * Time.deltaTime, 0f);
            image.color = c;
        }

        public void Activate() {
            Image image = GetComponent<Image>();
            if (!image) return;
            Color c = image.color;
            c.a = max_flash;
            image.color = c;
        }
    }
}