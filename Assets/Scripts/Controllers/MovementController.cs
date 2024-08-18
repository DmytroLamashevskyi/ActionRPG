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

    protected CharacterController _characterController;
    protected AnimatorController _animatorController;
    protected virtual void Awake()
    {
        _characterController = GetComponent<CharacterController>();
        _animatorController = new AnimatorController(GetComponent<Animator>());
    }

    protected virtual void MoveCharacter()
    {
        _movementVelocity.Normalize(); // Нормализуем вектор, чтобы избежать ускорения по диагонали

        // Применяем вращение в направлении движения
        if(_movementVelocity != Vector3.zero)
        {
            var targetRotation = Quaternion.LookRotation(_movementVelocity);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }

        // Обновляем скорость анимации
        _animatorController.SetSpeed(_movementVelocity.magnitude);
        // Учитываем вертикальную скорость
        _movementVelocity += _verticalVelocity * Vector3.up;

        // Двигаем CharacterController
        _characterController.Move(_movementVelocity * speed * Time.deltaTime);

    }

    protected virtual void ApplyGravity()
    {
        if(_characterController.isGrounded)
        {
            _verticalVelocity = -gravity * 0.3f; // Небольшая сила притяжения, чтобы держать персонажа на земле
        }
        else
        {
            _verticalVelocity -= gravity * Time.deltaTime; // Увеличиваем силу падения с течением времени
        }
        _animatorController.SetFall(!_characterController.isGrounded); 
    }

    protected virtual void FixedUpdate()
    {
        MoveCharacter();
        ApplyGravity();
    }
}
