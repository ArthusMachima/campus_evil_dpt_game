using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagPickup : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        PickUpHandItems player = other.GetComponent<PickUpHandItems>();
        if (player != null && player.isGun)
        {
            player.MagPickup();   // flip the flag
            this.gameObject.SetActive(false); // remove the gun pickup from the scene
        }
    }
}
