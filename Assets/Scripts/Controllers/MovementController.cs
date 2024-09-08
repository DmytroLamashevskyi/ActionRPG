using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(Animator))]
public class MovementController : MonoBehaviour
{
    public float speed = 5.0f;
    public float rotationSpeed = 2f;
    public float gravity = 9.8f;
    protected float _verticalVelocity;
    protected Vector3 _movementVelocity;
    protected float _speedScale = 1f;

    protected CharacterController _characterController;
    protected AnimatorController _animatorController;
    protected virtual void Awake()
    {
        _characterController = GetComponent<CharacterController>();
        _animatorController = new AnimatorController(GetComponent<Animator>());
    }

    protected virtual void MoveCharacter()
    {
        _movementVelocity.Normalize(); 

        if(_movementVelocity != Vector3.zero)
        {
            var targetRotation = Quaternion.LookRotation(_movementVelocity);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
         
        _animatorController.SetSpeed(_movementVelocity.magnitude); 
        _movementVelocity += _verticalVelocity * Vector3.up;
         
        _characterController.Move(_movementVelocity * speed * _speedScale * Time.deltaTime); 
    }

    protected virtual void ApplyGravity()
    {
        if(_characterController.isGrounded)
        {
            _verticalVelocity = -gravity * 0.3f;
        }
        else
        {
            _verticalVelocity -= gravity * Time.deltaTime;
        }
        _animatorController.SetFall(!_characterController.isGrounded); 
    }

    protected virtual void FixedUpdate()
    {
        MoveCharacter();
        ApplyGravity();
    }
}
