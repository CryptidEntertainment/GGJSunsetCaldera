using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonQuit : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
    public Sprite baseSprite;
    public Sprite altSprite;

    public void OnPointerEnter(PointerEventData eventData) {
        GetComponent<Image>().sprite = altSprite;
    }

    public void OnPointerExit(PointerEventData eventData) {
        GetComponent<Image>().sprite = baseSprite;
    }

    public void Quit() {
        Application.Quit();
    }
}
