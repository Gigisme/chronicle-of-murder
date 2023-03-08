using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    private PlayerInput _playerInput;

    private PlayerInput.OnFootActions _onFoot;

    private PlayerMotor _motor;

    private PlayerLook _look;

    // initialize
    void Awake()
    {
        _playerInput = new PlayerInput();
        _onFoot = _playerInput.OnFoot;
        _motor = GetComponent<PlayerMotor>();
        _onFoot.Jump.performed += ctx => _motor.Jump();
        _look = GetComponent<PlayerLook>();
    }
    
    void FixedUpdate()
    {
        //move player using value from input movement action
        _motor.ProcessMove(_onFoot.Movement.ReadValue<Vector2>());
    }

    private void LateUpdate()
    {
        //control player camera using value from input look action
        _look.ProcessLook(_onFoot.Look.ReadValue<Vector2>());;
    }

    private void OnEnable()
    {
        _onFoot.Enable();
    }

    private void OnDisable()
    {
        _onFoot.Disable();
    }
}
