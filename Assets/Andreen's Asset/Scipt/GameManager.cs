using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    

    [Header("Code Initials")]
    public TextMeshProUGUI passcodeScreen;
    public int passwordLimit = 4;
    public string codeInput;
    public int doorPassword;
    public GameObject doorSafeRoom;
    [Header("Code")]
    public static string code1;
    public static string code2;
    public static string code3;
    public static string code4;

    private void Awake()
    {
        // enforce single instance
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }


    



    void Start()
    {
        /*//Temp code
        StartCoroutine(TempClueDoor());*/

        doorPassword = Random.Range(1111, 9999);
        // Disable cursor at start
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        code1 = doorPassword.ToString()[0].ToString();
        code2 = doorPassword.ToString()[1].ToString();
        code3 = doorPassword.ToString()[2].ToString();
        code4 = doorPassword.ToString()[3].ToString();
    }


    



    //Pass code functions
    public void PassCodeDisplay(string code)
    {
        if (passcodeScreen.text.Length <= passwordLimit)
        {
            passcodeScreen.text += code;
            codeInput = passcodeScreen.text;
        }
        else
        {
            passcodeScreen.text = "";
            codeInput = passcodeScreen.text;
            Debug.Log("reach limit.");
        }
    }

    public void EnterPassword()
    {
        int code;
        if (int.TryParse(codeInput, out code))
        {
            DoorPasscode(code);
        }
        else
        {
            Debug.Log("Invalid input: not a number");
        }
    }

    void DoorPasscode(int code)
    {
        if (doorPassword == code)
        {
            doorSafeRoom.SetActive(false);
        }
    }



    
}
