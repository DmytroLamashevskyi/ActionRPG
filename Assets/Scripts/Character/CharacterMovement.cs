using System;
using UnityEngine;

namespace CharacterMechanics
{
    public class CharacterMovement : MonoBehaviour
    {
        private CharacterController _characterController;
        private CharacterAnimator _characterAnimator;
        private bool _isAttacking = false;

        public float walkSpeed = 2f;
        public float runSpeed = 6f;
        public float jumpHeight = 1.5f;
        public float gravity = -9.81f;
        public float rotationSpeed = 10f;
        public float attackSpeedMultiplier = 0.5f;  // Скорость во время атаки

        private Vector3 _moveDirection;
        private Vector3 _velocity;
        private bool _isGrounded;

        public Transform groundCheck;
        public float groundDistance = 0.4f;
        public LayerMask groundMask;

        private void Start()
        {
            _characterController = GetComponent<CharacterController>();
            _characterAnimator = GetComponent<CharacterAnimator>();
        }

        private void Update()
        {
            // Проверяем, находится ли персонаж на земле
            _isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

            if(_isGrounded && _velocity.y < 0)
            {
                _velocity.y = -2f;  // Удерживаем персонажа на земле
            }

            // Применение гравитации
            _velocity.y += gravity * Time.deltaTime;
            _characterController.Move(_velocity * Time.deltaTime);
        }

        public void Move(Vector3 direction, bool isRunning)
        {
            float speed = isRunning ? runSpeed : walkSpeed;
            if(_isAttacking)
            {
                speed *= attackSpeedMultiplier;  // Уменьшаем скорость во время атаки
            }

            _moveDirection = direction * speed;
            _characterController.Move(_moveDirection * Time.deltaTime);

            if(_moveDirection != Vector3.zero)
            {
                RotateTowardsDirection(_moveDirection);
            }

            _characterAnimator.UpdateMovementAnimation(_moveDirection.magnitude, isRunning);
        }

        private void RotateTowardsDirection(Vector3 direction)
        {
            var targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }

        public void Jump()
        {
            if(_isGrounded)
            {
                _velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
                _characterAnimator.PlayJumpAnimation();
            }
        }

        public void Stop()
        {
            _moveDirection = Vector3.zero;
            _characterController.Move(_moveDirection);
            _characterAnimator.UpdateMovementAnimation(0, false);
        }

        // Начало атаки
        public void StartAttack()
        {
            _isAttacking = true;
        }

        // Завершение атаки
        public void EndAttack()
        {
            _isAttacking = false;
        }
        public Vector3 GetMoveDirection()
        {
            return _moveDirection.normalized;
        }
    }
}
