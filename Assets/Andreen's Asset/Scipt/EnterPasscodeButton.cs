using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterPasscodeButton : Interactable
{
    public string digit;


    protected override void Interact()
    {
        Debug.Log("Pressed key: " + digit);
        // Send digit to keypad manager
        GameManager.Instance.EnterPassword();

    }
}
