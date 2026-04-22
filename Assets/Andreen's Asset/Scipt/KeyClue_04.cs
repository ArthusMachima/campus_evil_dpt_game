using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyClue_04 : Interactable
{
    private void Start()
    {
        StartCoroutine(StoreCountdown());
    }

    IEnumerator StoreCountdown()
    {
        yield return new WaitForSeconds(1.7f);
        prompMessage = "Fourth code: " + GameManager.code4;
    }
}
