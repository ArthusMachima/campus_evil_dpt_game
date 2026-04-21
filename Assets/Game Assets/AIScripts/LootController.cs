using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootController : MonoBehaviour
{
    public GameObject aIBody;
    public GameObject gunMag;

    Vector3 aiLastPositionm;
    private void Start()
    {
        aIBody.SetActive(true);
        gunMag.SetActive(false);

        aiLastPositionm = aIBody.transform.localPosition;
    }

    public void LootAvail()
    {
        Vector3 newPos = aIBody.transform.position;

        newPos.y = 2.12f;

        gunMag.transform.position = newPos;

        aIBody.SetActive(false);
        gunMag.SetActive(true);
    }

    public void ReactivateBody()
    {
        aIBody.SetActive(true);
        aIBody.transform.position = aiLastPositionm; // Restore last position
        gunMag.SetActive(false);
    }
}
