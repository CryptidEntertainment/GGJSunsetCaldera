using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace Peng {
    public class Button : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
        public Sprite baseSprite;
        public Sprite altSprite;

        public void OnPointerEnter(PointerEventData eventData) {
            GetComponent<Image>().sprite = altSprite;
        }

        public void OnPointerExit(PointerEventData eventData) {
            GetComponent<Image>().sprite = baseSprite;
        }
    }
}