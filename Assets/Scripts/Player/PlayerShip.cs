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
            CheckWinCondition();
        }

        public void ActivateFuel() {
            fuel.SetActive(true);
            CheckWinCondition();
        }

        public void ActivateNavigation() {
            navigation.SetActive(true);
            CheckWinCondition();
        }

        public void ActivatePod() {
            pod.SetActive(true);
            CheckWinCondition();
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

        private void CheckWinCondition() {
            if (steering.activeInHierarchy && fuel.activeInHierarchy && navigation.activeInHierarchy && pod.activeInHierarchy) {
                Player.Me.GetComponent<Player>().EnterWinScreen();
            }
        }
    }
}