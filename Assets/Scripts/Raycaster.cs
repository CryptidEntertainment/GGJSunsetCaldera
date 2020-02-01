using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
namespace Scott
{
    public class Raycaster : MonoBehaviour
    {
        public static Ray ray;

        public static RaycastResult getRayCastHit()
        {
            PointerEventData ped;
            List<RaycastResult> hitList;
            GameObject target = null;
            RaycastResult closest = new RaycastResult();
            ray = Camera.main.ScreenPointToRay(GetCursorPosition());
            ped = new PointerEventData(null);
            ped.position = GetCursorPosition();
            hitList = new List<RaycastResult>();
            if (EventSystem.current != null) EventSystem.current.RaycastAll(ped, hitList);
            if (hitList.Count == 0) return closest;
            foreach (RaycastResult r in hitList)
            {
                if (target == null)
                {
                    closest = r;
                    target = closest.gameObject;
                    continue;
                }
                else if (closest.distance < r.distance)
                {
                    closest = r;
                }
            }
            return closest;

        }

        public static Vector3 GetCursorPosition()
        {
            Vector3 defaultCenter = new Vector3(Screen.width / 2, Screen.height / 2, 1);
            return defaultCenter;
        }

    }




}