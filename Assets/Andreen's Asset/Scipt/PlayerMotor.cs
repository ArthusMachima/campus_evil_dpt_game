using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerMotor : MonoBehaviour
{
    public static PlayerMotor Instance;

    [Header("Player Health")]
    public float currentHealth = 0;
    public float maxHealth = 100;

    public Slider healthBar;


    private CharacterController controller;
    private Vector3 playerVelocity;
    public float speed = 5.0f;

    [Header("Player Gravity")]
    public  bool isGrounded;
    public float gravity = -9.8f;

    [Header("Jump")]
    public float jumpHeight = 3.0f;

    [Header("Clues")]
    public GameObject tempGameInfo;
    InputManager inputManager;

    [Header("Flags")]
    public bool isDead;

    [Header("UI")]
    public GameObject menuUIBG;
    public GameObject restartUi;


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

    private void Start()
    {
        isDead = false;
        menuUIBG.SetActive(false);
        restartUi.SetActive(false);
        controller = GetComponent<CharacterController>();
        inputManager = GetComponent<InputManager>();
        currentHealth = maxHealth;

    }

    private void Update()
    {
        isGrounded = controller.isGrounded;
        if (inputManager.OnFoot.Interact.triggered && tempGameInfo.activeSelf) tempGameInfo.SetActive(false); 
    }

    //Recieve the inputs from out input manager scripts to our chracter controller
    public void ProcessMove(Vector3 input)
    {
        if (!isDead)
        {
            Vector3 moveDirection = Vector3.zero;
            moveDirection.x = input.x;
            moveDirection.z = input.y;

            controller.Move(transform.TransformDirection(moveDirection) * speed * Time.deltaTime);
            playerVelocity.y += gravity * Time.deltaTime;
            if (isGrounded && playerVelocity.y < 0)
            {
                playerVelocity.y = -2f;
            }
            controller.Move(playerVelocity * Time.deltaTime);
        }
        else
        {
            Debug.LogWarning("Player controls desables");
        }
       
    }

    public void Jump()
    {
        if (isGrounded)
        {
            playerVelocity.y = Mathf.Sqrt(jumpHeight * -3.0f * gravity);
        }
    }

    public void PlayerHealthBar(float damage)
    {
        if(currentHealth > damage)
        {
            currentHealth -= damage;
            currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
            UpdateHleathUI();
        }
        else
        {
            currentHealth = 0;
            UpdateHleathUI();
            PlayerDeath();
        }
    }

    void UpdateHleathUI()
    {
        if(healthBar != null)
        {
            healthBar.value = (float)currentHealth / maxHealth;
        }
    }

    void PlayerDeath()
    {
        Debug.LogWarning("Player has no health");
        isDead = true;
        RestartMenu();

    }

    void RestartMenu()
    {
        Cursor.lockState  = CursorLockMode.None;
        Cursor.visible = true;
        menuUIBG.SetActive(true);
        restartUi.SetActive(true);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(0);
    }
}
