using Game.AI; 
using UnityEngine;

public class WarriorEnemy : AiCharacter
{
    private void Start()
    {
        SetBehavior(new PatrolBehavior()); // Начальное поведение — патрулирование
    }

    public override void ActOnTarget()
    {
        if(IsTargetInActionRange())
        {
            Debug.Log("Воин атакует игрока в ближнем бою");
            AttackPlayer();
        }
        else
        {
            navAgent.destination = target.position;
            Debug.Log("Воин преследует игрока");
        }
    }

    private void AttackPlayer()
    {
        // Логика нанесения урона
        Debug.Log("Игрок получает урон от воина");
        // Здесь можно реализовать взаимодействие с системой здоровья игрока
    }
}
