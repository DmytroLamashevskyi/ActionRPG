using UnityEngine;

namespace Game.AI
{ 
    public class ChaseBehavior : IAIBehavior
    {
        public void EnterBehavior(AiCharacter character)
        {
            character.SetMovementSpeed(character.chaseSpeed);
            Debug.Log("Начало преследования");
        }

        public void ExecuteBehavior(AiCharacter character)
        {
            character.navAgent.destination = character.target.position;

            // Если цель в радиусе действия, переключаемся на атаку (или помощь для союзников)
            if(character.IsTargetInActionRange())
            {
                character.SetBehavior(new ActionBehavior());
            }
            // Если цель вышла за пределы обнаружения, возвращаемся к патрулированию
            else if(!character.IsTargetInDetectionRange())
            {
                character.SetBehavior(new PatrolBehavior());
            }
        }

        public void ExitBehavior(AiCharacter character)
        {
            Debug.Log("Выход из преследования");
        }
    }

}