using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerInput))]
[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(Animator))]
public class PlayerMovementController : MovementController
{
    private PlayerInput _playerInput;
    public float SpeedScale;

    protected override void Awake()
    {
        base.Awake();
        _playerInput = GetComponent<PlayerInput>();
    }

    protected override void MoveCharacter()
    {
        _speedScale = _playerInput.speedUp ? SpeedScale : 1; 
        _movementVelocity.Set(_playerInput.horizontalInput , 0, _playerInput.verticalInput );
        base.MoveCharacter(); 
    }
}
