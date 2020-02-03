using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Peng {
    public class PlayerShip : MonoBehaviour {
        public GameObject steering;
        public GameObject fuel;
        public GameObject navigation;
        public GameObject pod;

        public GameObject fuelNeeded;
        public GameObject fuelChecked;

        public GameObject navigationNeeded;
        public GameObject navigationChecked;

        public GameObject podNeeded;
        public GameObject podChecked;

        public GameObject steeringNeeded;
        public GameObject steeringChecked;

        void Awake() {
            DeactivateFuel();
            DeactivateNavigation();
            DeactivateSteering();
            // you can deactivate pod if you want, I guess?
        }

        public void ActivateSteering() {
            steering.SetActive(true);
            CheckWinCondition();
            steeringChecked.SetActive(true);
            steeringNeeded.SetActive(false);
        }

        public void ActivateFuel() {
            fuel.SetActive(true);
            CheckWinCondition();
            fuelChecked.SetActive(true);
            fuelNeeded.SetActive(false);
        }

        public void ActivateNavigation() {
            navigation.SetActive(true);
            CheckWinCondition();
            navigationChecked.SetActive(true);
            navigationNeeded.SetActive(false);
        }

        public void ActivatePod() {
            pod.SetActive(true);
            CheckWinCondition();
            podChecked.SetActive(true);
            podNeeded.SetActive(false);
        }

        public void DeactivateSteering() {
            steering.SetActive(false);
            steeringChecked.SetActive(false);
            steeringNeeded.SetActive(true);
        }

        public void DeactivateFuel() {
            fuel.SetActive(false);
            fuelChecked.SetActive(false);
            fuelNeeded.SetActive(true);
        }

        public void DeactivateNavigation() {
            navigation.SetActive(false);
            navigationChecked.SetActive(false);
            navigationNeeded.SetActive(true);
        }

        public void DeactivatePod() {
            pod.SetActive(false);
            podChecked.SetActive(false);
            podNeeded.SetActive(true);
        }

        private void CheckWinCondition() {
            if (steering.activeInHierarchy && fuel.activeInHierarchy && navigation.activeInHierarchy && pod.activeInHierarchy) {
                Player.Me.GetComponent<Player>().EnterWinScreen();
            }
        }
    }
}