using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Peng;

namespace Scott
{
    public class GravityGun : MonoBehaviour
    {
        RaycastResult rayHit;
        public float gravDistance;
        public float grabbingDistance;
        public GameObject cam;
        public float maxSnapDistance;
        public GameObject firePart;
        public AudioSource aS;
        
        private bool pickupActive;
        private bool fireDown;
        private GameObject grabTarget;
        private GameObject gravPoint;
        
        void Awake() {
            gravPoint = new GameObject("Gravity Point");
        }

        void Update()
        {
            switch (GetComponent<Player>().Mode) {
                case GameStates.TITLE:
                    break;
                case GameStates.PLAY:
                    UpdateGameplay();
                    break;
            }
            if (Input.GetAxis("Fire1")>0&&!pickupActive&&!fireDown)
            {
                fireDown=true;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, 50f))
                {
                    if (hit.collider.gameObject.GetComponent<InteractiveObject>() != null)
                    {
                        pickupActive = true;
                        grabTarget = hit.collider.gameObject;
                        grabTarget.GetComponent<InteractiveObject>().pickup(gravPoint, this);
                    }
                }
                
            }
            else if (Input.GetAxis("Fire1")>0&&pickupActive&&!fireDown)
            {
                fireDown = true;
                pickupActive = false;
                grabTarget.GetComponent<InteractiveObject>().drop();
            }
        }
        
        public void autoDrop()
        {
            pickupActive = false;
            grabTarget.GetComponent<InteractiveObject>().drop();
            grabTarget = null;
            aS.loop = false;
        }

        public void grabbedDestroyed()
        {
            pickupActive = false;
            grabTarget = null;
            aS.loop = false;
        }

        private void UpdateGameplay() {
            gravPoint.transform.position = cam.transform.position + cam.transform.forward * gravDistance;
            if(pickupActive)
            {

            }
            // release
            if (Input.GetAxis("Fire1") < 1 && fireDown) {
                fireDown = false;
            }
            // press - grab
            if (Input.GetAxis("Fire1") > 0 && !pickupActive && !fireDown) {
                if (firePart.activeInHierarchy) {
                    firePart.SetActive(false);
                }
                firePart.SetActive(true);
                aS.Play();
                fireDown = true;
                Ray ray = cam.GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, grabbingDistance)) {
                    if (hit.collider.gameObject.GetComponent<InteractiveObject>() != null) {
                        pickupActive = true;
                        aS.loop = true;
                        grabTarget = hit.collider.gameObject;
                        grabTarget.GetComponent<InteractiveObject>().pickup(gravPoint, this);
                    }
                }
            // press - fling
            } else if (Input.GetAxis("Fire1") > 0 && pickupActive && !fireDown) {
                fireDown = true;
                pickupActive = false;
                aS.loop = false;
                grabTarget.GetComponent<InteractiveObject>().drop();
                grabTarget = null;
            }
        }
    }
}