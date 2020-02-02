using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanumMortum : MonoBehaviour {
    void OnTriggerEnter(Collider c) {
        IMortal poorSap = c.gameObject.GetComponent<IMortal>();
        if (poorSap != null) {
            poorSap.Die();
        }
    }
}