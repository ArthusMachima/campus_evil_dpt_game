using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyClue_03 : Interactable
{
    private void Start()
    {
        StartCoroutine(StoreCountdown());
    }

    IEnumerator StoreCountdown()
    {
        yield return new WaitForSeconds(1.7f);
        prompMessage = "Third code: " + GameManager.code3;
    }
}
