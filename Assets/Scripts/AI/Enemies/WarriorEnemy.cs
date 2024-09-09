using CharacterMechanics;
using Game.AI;
using UnityEngine;
using UnityEngine.AI;

public class WarriorEnemy : AiCharacter
{  
    private void Start()
    { 
        SetBehavior(new PatrolBehavior());
    }

    private void Update()
    {
        ActOnTarget();
    }

    public override void ActOnTarget()
    {
        if(IsTargetInActionRange())
        {
            AttackPlayer();  // Атакуем игрока
        }
        else if(IsTargetInDetectionRange())
        {
            ChasePlayer();  // Преследуем игрока
        }
        else
        {
            Patrol();  // Возвращаемся к патрулированию
        }
    }

    private void ChasePlayer()
    {
        if(target != null)
        {
            // Устанавливаем цель для преследования с помощью NavMeshAgent
            navAgent.SetDestination(target.position);

            if(navAgent.remainingDistance > navAgent.stoppingDistance)
            {
                // Вычисляем направление движения
                Vector3 direction = navAgent.desiredVelocity.normalized;

                // Передвигаем персонажа через CharacterMovement
                _characterMovement.Move(direction,false); 
                Debug.Log("Воин преследует игрока, направление: " + direction);
            }
            else
            {
                // Останавливаем персонажа
                _characterMovement.Stop();
                Debug.Log("Воин остановился перед игроком");
            }
        }
    } 
    private void AttackPlayer()
    {
        // Логика нанесения урона
        Debug.Log("Игрок получает урон от воина");
    }
}
