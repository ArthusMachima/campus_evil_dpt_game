using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerUI : MonoBehaviour
{
    public TextMeshProUGUI promptText;


    public void UpdateText(string propmpMesssage)
    {
        promptText.text = propmpMesssage;
    }
}
