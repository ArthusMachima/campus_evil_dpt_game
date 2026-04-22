using UnityEngine;

public class GunPickup : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        PickUpHandItems player = other.GetComponent<PickUpHandItems>();
        if (player != null)
        {
            player.EquipGun();   // flip the flag
            this.gameObject.SetActive(false); // remove the gun pickup from the scene
        }
    }
}
