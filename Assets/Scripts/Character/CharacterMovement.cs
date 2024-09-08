using UnityEngine;

namespace CharacterMechanics
{ 
    public class CharacterMovement : MonoBehaviour
    {
        private CharacterController _characterController;
        private CharacterAnimator _characterAnimator;

        public float walkSpeed = 2f;
        public float runSpeed = 6f;
        public float jumpHeight = 1.5f;
        public float gravity = -9.81f;
        public float rotationSpeed = 10f;  // Скорость поворота

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
            // Проверка, на земле ли персонаж
            _isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

            if(_isGrounded && _velocity.y < 0)
            {
                _velocity.y = -2f;  // Держим персонажа на земле
            }

            // Применение гравитации
            _velocity.y += gravity * Time.deltaTime;
            _characterController.Move(_velocity * Time.deltaTime);
        }

        public void Move(Vector3 direction, bool isRunning)
        {
            float speed = isRunning ? runSpeed : walkSpeed;
            _moveDirection = direction * speed;
            _characterController.Move(_moveDirection * Time.deltaTime);

            // Поворачиваем игрока в сторону движения
            if(_moveDirection != Vector3.zero)
            {
                RotateTowardsDirection(_moveDirection);
            }

            // Обновляем анимацию движения
            _characterAnimator.UpdateMovementAnimation(_moveDirection.magnitude, isRunning);
        }

        private void RotateTowardsDirection(Vector3 direction)
        {
            // Рассчитываем целевую ротацию на основе вектора направления
            var targetRotation = Quaternion.LookRotation(direction);
            // Плавно поворачиваем персонажа в сторону движения
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }

        public void Jump()
        {
            if(_isGrounded)
            {
                _velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);  // Рассчитываем силу прыжка
                _characterAnimator.PlayJumpAnimation();  // Запуск анимации прыжка
            }
        }

        public void Stop()
        {
            _moveDirection = Vector3.zero;
            _characterController.Move(_moveDirection);
            _characterAnimator.UpdateMovementAnimation(0, false);  // Останавливаем анимацию
        }

        public void Attack()
        {
            _characterAnimator.PlayAttackAnimation();  // Запуск анимации атаки
        }

        public void EndAttack()
        {
            _characterAnimator.StopAttackAnimation();  // Завершение анимации атаки
        }

        public void SetFalling(bool isFalling)
        {
            _characterAnimator.PlayFallingAnimation(isFalling);  // Управление анимацией падения
        }

        public float GetCurrentSpeed()
        {
            return _moveDirection.magnitude;
        }
    }

}