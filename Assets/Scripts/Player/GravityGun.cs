using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Scott
{
    public class GravityGun : MonoBehaviour
    {
        private bool pickupActive;
        private GameObject grabTarget;

        void Update()
        {
            if (Input.GetAxis("Fire1")>0)
            {
                grabTarget = Raycaster.getRayCastHit().gameObject;
                grabTarget.GetComponent<InteractiveObject>();
            }
        }

    }
}