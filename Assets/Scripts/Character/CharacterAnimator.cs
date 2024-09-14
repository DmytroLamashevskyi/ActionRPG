using System;
using Unity.Mathematics;
using UnityEngine;

namespace CharacterMechanics
{
    public class CharacterAnimator : MonoBehaviour
    {
        private Animator _animator;

        private void Start()
        {
            _animator = GetComponent<Animator>();  // Инициализируем аниматор
        }

        // Обновление анимации движения (Idle, Walk, Run)
        public void UpdateMovementAnimation(float speed, bool isRunning)
        {
            _animator.SetFloat("speed", speed);
            _animator.SetBool("isRunning", isRunning);
        }

        // Запуск анимации прыжка
        public void PlayJumpAnimation()
        {
            _animator.SetTrigger("jump");
        }

        // Запуск анимации атаки
        public void PlayAttackAnimation()
        {
            // Проверяем, не была ли уже запущена атака
            if(!_animator.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
            {
                _animator.SetInteger("attackIndex", UnityEngine.Random.Range(0, 2));
                _animator.SetTrigger("Attack");  // Запускаем триггер только если не в состоянии атаки
            }
        }

        // Завершение анимации атаки
        public void StopAttackAnimation()
        {
            _animator.ResetTrigger("Attack");  // Сбрасываем триггер после завершения атаки
        }

        // Запуск анимации падения
        public void PlayFallingAnimation(bool isFalling)
        {
            _animator.SetBool("isFalling", isFalling);
        }

        // Запуск анимации смерти
        public void PlayDeathAnimation()
        {
            _animator.SetTrigger("die");
        }

        internal void SetAttackAnimationSpeed(float animationSpeedMultiplier)
        {
            _animator.SetFloat("AttackSpeed", animationSpeedMultiplier);
        }
    }

}
