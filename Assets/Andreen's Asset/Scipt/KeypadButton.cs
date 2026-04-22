using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeypadButton : Interactable
{
    public string digit;


    protected override void Interact()
    {
        Debug.Log("Pressed key: " + digit);
        // Send digit to keypad manager
        GameManager.Instance.PassCodeDisplay(digit);
        
    }
}

