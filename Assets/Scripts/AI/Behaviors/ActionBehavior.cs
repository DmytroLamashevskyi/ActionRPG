using UnityEngine;

namespace Game.AI
{

    public class ActionBehavior : IAIBehavior
    {
        public void EnterBehavior(AiCharacter character)
        {
            Debug.Log("Начало действия");
        }

        public void ExecuteBehavior(AiCharacter character)
        {
            character.ActOnTarget(); // Вызов уникальной логики для врагов или союзников

            // Если цель больше не в радиусе действия, переключаемся на преследование
            if(!character.IsTargetInActionRange())
            {
                character.SetBehavior(new ChaseBehavior());
            }
        }

        public void ExitBehavior(AiCharacter character)
        {
            Debug.Log("Выход из действия");
        }
    }

}