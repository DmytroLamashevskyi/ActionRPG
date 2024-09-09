using UnityEngine;

namespace CharacterMechanics
{
    public class CharacterAttack : MonoBehaviour
    {
        private CharacterAnimator _characterAnimator;
        private CharacterMovement _characterMovement;  // Ссылка на аниматор (если используется анимация)

        public float attackSpeed = 1.0f;  // Скорость атаки, время выполнения одной атаки

        private void Start()
        {
            _characterAnimator = GetComponent<CharacterAnimator>();
            _characterMovement = GetComponent<CharacterMovement>();
        }

        // Метод для атаки
        public void Attack()
        {
            _characterMovement.StartAttack();
            // Запуск анимации атаки
            if(_characterAnimator != null)
            {
                _characterAnimator.PlayAttackAnimation();
                SyncAttackSpeedWithAnimation();
            }

            // Логика атаки
            Debug.Log("Player is Attacking!");
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

        // Метод для получения времени завершения атаки
        public float GetAttackSpeed()
        {
            return attackSpeed;
        }
    }
}
