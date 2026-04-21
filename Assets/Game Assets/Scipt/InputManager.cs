using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    private PlayerInput playerInput;
    private PlayerInput.OnfootActions onfoot;
    private PlayerMotor motor;
    private PlayerLook look;
    public PlayerInput.OnfootActions OnFoot => onfoot;

    private void Awake()
    {

        playerInput = new PlayerInput();
        onfoot = playerInput.Onfoot;
        motor = GetComponent<PlayerMotor>();
        look = GetComponent<PlayerLook>();
        onfoot.Jump.performed += ctx => motor.Jump();
    }

    private void FixedUpdate()
    {
        //tell the playermotor
        motor.ProcessMove(onfoot.Movement.ReadValue<Vector2>());
    }

    private void LateUpdate()
    {
        look.ProcessLook(onfoot.Look.ReadValue<Vector2>());
    }

    private void OnEnable()
    {
        onfoot.Enable();
    }

    private void OnDisable()
    {
        onfoot.Disable();
    }
}
