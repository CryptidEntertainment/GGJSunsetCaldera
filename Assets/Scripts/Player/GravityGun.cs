using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
namespace Scott
{
    public class GravityGun : MonoBehaviour
    {
        private bool pickupActive;
        RaycastResult rayHit;
        private GameObject grabTarget;
        private bool fireDown;
        public float gravDistance;
        public GameObject cam;
        public GameObject gravPoint;
        public float maxSnapDistance;

        void Update()
        {
            gravPoint.transform.position = cam.transform.position + cam.transform.forward * gravDistance;
            if (Input.GetAxis("Fire1")<1&&fireDown)
            {
                fireDown = false;
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
        }

    }
}