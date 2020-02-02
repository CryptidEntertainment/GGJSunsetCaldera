using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Scott
{
    public class ShipRepair : MonoBehaviour
    {
        Peng.Player player;
        Peng.PlayerShip playerShip;
        Scott.ShipPart shipPart;
        Scott.GravityGun gravGun;

        void Start()
        {
            player = Peng.Player.Me;
            playerShip = player.GetComponent<Peng.PlayerShip>();
            player.GetComponent<GravityGun>();
        }

        // Update is called once per frame
        void OnTriggerEnter(Collider coll)
        {
            shipPart = coll.gameObject.GetComponent<ShipPart>();
            if (shipPart != null)
            {
                switch (shipPart.getType())
                {
                    case "Steering":
                        playerShip.ActivateSteering();
                        break;
                    case "Fuel":
                        playerShip.ActivateFuel();
                        break;
                    case "Navigation":
                        playerShip.ActivateNavigation();
                        break;
                    default:
                        Debug.Log("The thing isn't labeled right!");
                        break;
                }
                gravGun.grabbedDestroyed();
                Destroy(coll.gameObject);
            }
        }
    }
}