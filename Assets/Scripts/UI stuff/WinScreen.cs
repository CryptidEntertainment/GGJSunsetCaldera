using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinScreen : MonoBehaviour {
    public GameObject[] gameplayStuff;

    public void Activate() {
        gameObject.SetActive(true);

        foreach (GameObject thing in gameplayStuff) {
            thing.SetActive(false);
        }
    }
}