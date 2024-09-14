using UnityEngine;

namespace CharacterMechanics
{
    public class CharacterAttack : MonoBehaviour
    {
        private CharacterAnimator _characterAnimator;
        private CharacterMovement _characterMovement;

        public float attackSpeed ;  // Скорость атаки, время выполнения одной атаки
        public float attackCooldown;  // Кулдаун между атаками
        private bool _canAttack = true;  // Проверка, можно ли атаковать

        private void Start()
        {
            _characterAnimator = GetComponent<CharacterAnimator>();
            _characterMovement = GetComponent<CharacterMovement>();
        }

        // Метод для атаки
        public void Attack()
        {
            if(_canAttack)
            {
                _canAttack = false;

                _characterMovement.StartAttack();

                // Запуск анимации атаки
                if(_characterAnimator != null)
                {
                    _characterAnimator.PlayAttackAnimation();
                    SyncAttackSpeedWithAnimation();
                }

                // Включение хитбоксов оружия (если используется)
                EnableHitbox();

                // Логика атаки
                Debug.Log("Player is Attacking!");

                // Запускаем кулдаун на следующую атаку
                Invoke("ResetAttack", attackSpeed + attackCooldown);
            }
        }

        // Метод для завершения атаки
        public void EndAttack()
        {
            _characterMovement.EndAttack();

            // Завершение анимации атаки
            if(_characterAnimator != null)
            {
                _characterAnimator.StopAttackAnimation();
            }

            // Отключение хитбоксов оружия (если используется)
            DisableHitbox();

            // Логика завершения атаки
            Debug.Log("Player has finished Attacking.");
        }

        // Метод для синхронизации скорости атаки и анимации
        private void SyncAttackSpeedWithAnimation()
        {
            if(_characterAnimator != null)
            {
                float animationSpeedMultiplier = 1 / attackSpeed;  // Инверсируем скорость, чтобы синхронизировать
                _characterAnimator.SetAttackAnimationSpeed(animationSpeedMultiplier);
            }
        }

        // Метод для включения хитбокса
        private void EnableHitbox()
        {
            // Логика для включения хитбокса оружия
            Debug.Log("Hitbox enabled.");
        }

        // Метод для отключения хитбокса
        private void DisableHitbox()
        {
            // Логика для отключения хитбокса оружия
            Debug.Log("Hitbox disabled.");
        }

        // Метод для сброса возможности атаковать (после кулдауна)
        private void ResetAttack()
        {
            _canAttack = true;
        }

        // Метод для получения времени завершения атаки
        public float GetAttackSpeed()
        {
            return attackSpeed;
        }
    }
}
