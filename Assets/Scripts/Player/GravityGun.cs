using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Scott
{
    public class GravityGun : MonoBehaviour
    {
        private bool pickupActive;
        private Raycaster rc = GetComponent<Raycaster>();
        private GameObject grabTarget;

        void Update()
        {
            if (Input.GetAxis("Fire1")>0)
            {
                grabTarget = rc.getRayCastHit().gameObject;
                grabTarget.getComponent<InteractiveObject>();
            }
        }

    }
}