using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Keypad : Interactable
{
    public bool doorOpen;


    protected override void Interact()
    {
        Debug.Log("The door is open.");
        doorOpen = !doorOpen;
        //door.GetComponent<Animator>().SetBool("IsOpen", doorOpen);
    }
}
