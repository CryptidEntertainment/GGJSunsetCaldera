using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Scott
{
    public class InteractiveObject : MonoBehaviour, IMortal
    {
        protected bool interacting;
        public float weight;
        protected GameObject moveTarget;
        private Vector3 prevPos;

        private Vector3 currentPos;
        private float lerpRate;
        private float fixedTime;
        private Vector3 throwVelocity;
        private Vector3 lastValid;
        GravityGun callback;
        private Vector3 originPosition;
        private Quaternion originRotation;
        
        void Awake() {
            originPosition = transform.position;
            originRotation = transform.rotation;
        }

        public void pickup(GameObject obj,GravityGun cb)
        {
            interacting = true;
            lastValid = transform.position;
            lerpRate = 1 / (Mathf.Log(weight * weight));
            if (GetComponent<Rigidbody>())
            {
                GetComponent<Rigidbody>().useGravity = false;
                GetComponent<Rigidbody>().velocity = Vector3.zero;
            }
            moveTarget = obj;
            callback = cb;
        }

        public void drop()
        {
            interacting = false;
            if (GetComponent<Rigidbody>())
            {
                GetComponent<Rigidbody>().useGravity = true;
                throwVelocity = new Vector3((currentPos.x - prevPos.x) / fixedTime, (currentPos.y - prevPos.y) / fixedTime, (currentPos.z - prevPos.z) / fixedTime);
                GetComponent<Rigidbody>().velocity = throwVelocity;
            }
        }

        void FixedUpdate()
        {
            if (interacting)
            {
                prevPos = currentPos;
                currentPos = transform.position;
                if (!Physics.Linecast(transform.position, moveTarget.transform.position))
                {
                    this.transform.position = new Vector3(Mathf.Lerp(currentPos.x, moveTarget.transform.position.x, lerpRate), Mathf.Lerp(currentPos.y, moveTarget.transform.position.y, lerpRate), Mathf.Lerp(currentPos.z, moveTarget.transform.position.z, lerpRate));
                    lastValid = this.transform.position;
                }
                else
                {
                    if (Vector3.Distance(this.transform.position, moveTarget.transform.position) < callback.maxSnapDistance)

                    {
                        this.transform.position = new Vector3(Mathf.Lerp(currentPos.x, lastValid.x, lerpRate), Mathf.Lerp(currentPos.y, lastValid.y, lerpRate), Mathf.Lerp(currentPos.z, lastValid.z, lerpRate));
                    }
                    else
                    {
                        callback.autoDrop();
                    }
                }
                fixedTime = Time.deltaTime;
            }
        }

        /// <summary>
        /// Methods required by IMortal
        /// </summary>

        public virtual void Die() {
            Rigidbody rb = GetComponent<Rigidbody>();
            if (rb) {
                rb.velocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
            }
            transform.position = originPosition;
            transform.rotation = originRotation;
        }
    }
}