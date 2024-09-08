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
            _animator.SetBool("isAttacking", true);
        }

        public void StopAttackAnimation()
        {
            _animator.SetBool("isAttacking", false);
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
    }

}
