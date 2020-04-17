using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace Peng {
    public class ButtonPlay : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
        public Sprite baseSprite;
        public Sprite altSprite;
        public GameObject[] titleStuff;
        public GameObject[] gameplayStuff;

        public void OnPointerEnter(PointerEventData eventData) {
            GetComponent<Image>().sprite = altSprite;
        }

        public void OnPointerExit(PointerEventData eventData) {
            GetComponent<Image>().sprite = baseSprite;
        }

        public void Play() {
            Player.Me.EnterPlayMode();
            foreach (GameObject thing in titleStuff) {
                thing.SetActive(false);
            }
            foreach (GameObject thing in gameplayStuff) {
                thing.SetActive(true);
            }
        }
    }
}