using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    private InputActions playerInput;
    private InputActions.OnFootActions onFoot;
    private PlayerMotor motor;
    private PlayerLook look;
    private PlayerAttack attack;
    void Awake() 
    {
        playerInput = new InputActions();
        onFoot = playerInput.OnFoot;

        // Inputs used in the game
        motor = GetComponent<PlayerMotor>();
        look = GetComponent<PlayerLook>();
        attack = GetComponent<PlayerAttack>();
        
        AssignInputs();
    }

    void AssignInputs()
    {
        onFoot.Jump.performed += ctx => motor.Jump();
        onFoot.Crouch.performed += ctx => motor.Crouch();
        onFoot.Sprint.performed += ctx => motor.Sprint();
        onFoot.Attack.performed += ctx => attack.Attack();
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // This function is called every fixed framerate frame, if the MonoBehaviour is enabled.
    void FixedUpdate()
    {
        // tell the playermotor to move using the value from our movement action
        motor.ProcessMove(onFoot.Movement.ReadValue<Vector2>() );
    }

    private void LateUpdate() 
    {
        look.ProcessLook(onFoot.Look.ReadValue<Vector2>() );
        
    }

    private void OnEnable() 
    {
        onFoot.Enable();
    }

    private void OnDisable() 
     {
        onFoot.Disable();
    }
}
