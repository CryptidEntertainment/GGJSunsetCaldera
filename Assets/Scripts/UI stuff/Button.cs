using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace Peng {
    public class Button : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
        public Sprite baseSprite;
        public Sprite altSprite;

        public void OnPointerEnter(PointerEventData eventData) {
            SetAltSprite();
        }

        public void OnPointerExit(PointerEventData eventData) {
            SetBaseSprite();
        }

        public void SetAltSprite() {
            GetComponent<Image>().sprite = altSprite;
        }

        public void SetBaseSprite() {
            GetComponent<Image>().sprite = baseSprite;
        }
    }
}