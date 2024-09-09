using UnityEngine;

namespace Game.AI
{
    public class PatrolBehavior : IAIBehavior
    {
        private bool _isWaiting = false;
        private float _waitTimer = 0f;

        public void EnterBehavior(AiCharacter character)
        {
            character.SetMovementSpeed(character.patruleSpeed); 
            character.SetNextPatrolPoint();  
        }

        public void ExecuteBehavior(AiCharacter character)
        {
            // Если цель в радиусе обнаружения, переключаемся на преследование
            if(character.IsTargetInDetectionRange())
            {
                character.SetBehavior(new ChaseBehavior());
                return;
            }

            // Логика ожидания на патрульной точке
            if(_isWaiting)
            {
                _waitTimer += Time.deltaTime;

                if(_waitTimer >= character.patrolWaitTime)
                {
                    _isWaiting = false;
                    _waitTimer = 0f;
                    character.SetNextPatrolPoint();
                }
            }
            else
            {
                // Если враг достиг текущей точки патрулирования
                if(character.navAgent.remainingDistance <= character.navAgent.stoppingDistance)
                {
                    _isWaiting = true;
                    Debug.Log("Ожидание на патрульной точке");
                }
            }
        }

        public void ExitBehavior(AiCharacter character)
        {
            Debug.Log("Выход из патрулирования");
        } 
    }
}
