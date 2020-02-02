using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Peng {
    public class PlayerShip : MonoBehaviour {
        public GameObject steering;
        public GameObject fuel;
        public GameObject navigation;
        public GameObject pod;

        void Awake() {
            DeactivateFuel();
            DeactivateNavigation();
            DeactivateSteering();
            // you can deactivate pod if you want, I guess?
        }

        public void ActivateSteering() {
            steering.SetActive(true);
        }

        public void ActivateFuel() {
            fuel.SetActive(true);
        }

        public void ActivateNavigation() {
            navigation.SetActive(true);
        }

        public void ActivatePod() {
            pod.SetActive(true);
        }

        public void DeactivateSteering() {
            steering.SetActive(false);
        }

        public void DeactivateFuel() {
            fuel.SetActive(false);
        }

        public void DeactivateNavigation() {
            navigation.SetActive(false);
        }

        public void DeactivatePod() {
            pod.SetActive(false);
        }
    }
}