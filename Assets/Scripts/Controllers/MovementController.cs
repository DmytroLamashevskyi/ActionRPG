using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerInput))]
[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(Animator))]
public class MovementController : MonoBehaviour
{
    public float speed = 5.0f;
    public float gravity = 9.8f;
    private float _verticalVelocity;
    private Vector3 _movementVelocity;

    private PlayerInput _playerInput;
    private CharacterController _characterController;
    private AnimatorController _animatorController;
    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
        _playerInput = GetComponent<PlayerInput>();
        _animatorController = new AnimatorController(GetComponent<Animator>());
    }

    private void CalculateMovement()
    {
        _movementVelocity.Set(_playerInput.horizontalInput,0,_playerInput.verticalInput);
        _movementVelocity.Normalize();
        _movementVelocity = Quaternion.Euler(0,-45f,0) * _movementVelocity;
        _animatorController.SetSpeed(_movementVelocity.magnitude);
        _movementVelocity *= speed * Time.deltaTime; 
        if(_movementVelocity != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(_movementVelocity);
        }
        _animatorController.SetFall(!_characterController.isGrounded); 
        if( !_characterController.isGrounded )
        {
            _verticalVelocity = -gravity;
        }
        else
        { 
            _verticalVelocity = -gravity * 0.3f;
        }
        _movementVelocity += _verticalVelocity * Vector3.up * Time.deltaTime; 
        _characterController.Move(_movementVelocity);
    } 

    private void FixedUpdate()
    {
        CalculateMovement();
    }
}
