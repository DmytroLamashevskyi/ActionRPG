using UnityEngine;

namespace Game.AI
{ 
    public class PatrolBehavior : IAIBehavior
    {
        public void EnterBehavior(AiCharacter character)
        {
            character.SetMovementSpeed(character.patruleSpeed);
            character.Patrol(); // Вход в режим патрулирования
        }

        public void ExecuteBehavior(AiCharacter character)
        {
            // Если цель в радиусе обнаружения, переключаемся на другое поведение
            if(character.IsTargetInDetectionRange())
            {
                character.SetBehavior(new ChaseBehavior());
            }
        }

        public void ExitBehavior(AiCharacter character)
        {
            Debug.Log("Выход из патрулирования");
        }
    }

}