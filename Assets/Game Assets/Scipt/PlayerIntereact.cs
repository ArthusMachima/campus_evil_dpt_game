using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIntereact : MonoBehaviour
{
    private Camera cam;
    public float distance = 3f;
    public LayerMask mask;
    private PlayerUI playerUI;
    private InputManager input;


    private void Start()
    {
        cam = GetComponent<PlayerLook>().cam;
        playerUI = GetComponent<PlayerUI>();
        input = GetComponent<InputManager>();
    }

    private void Update()
    {
        playerUI.UpdateText(string.Empty);
        //create a ray at the cetner of the camera, shooting outwards.
        Ray ray = new Ray(cam.transform.position, cam.transform.forward);
        Debug.DrawRay(ray.origin, ray.direction * distance);

        RaycastHit hitInfo; //variable to store our collision information
        if (Physics.Raycast(ray, out hitInfo, distance, mask))
        {
            if (hitInfo.collider.GetComponent<Interactable>() != null)
            {
                Interactable interactable = hitInfo.collider.GetComponent<Interactable>();
                playerUI.UpdateText(interactable.prompMessage);
                if (input.OnFoot.Interact.triggered)
                {
                    interactable.BaseInteract();
                }
            }
        }
    }
}
