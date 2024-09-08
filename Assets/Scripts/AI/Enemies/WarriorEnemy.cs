using CharacterMechanics;
using Game.AI;
using UnityEngine;
using UnityEngine.AI;

public class WarriorEnemy : AiCharacter
{
    private NavMeshAgent _navAgent;
    private CharacterMovement _characterMovement;

    private void Start()
    {
        _navAgent = GetComponent<NavMeshAgent>();
        _characterMovement = GetComponent<CharacterMovement>();

        // Отключаем автоуправление позицией и вращением у NavMeshAgent
        _navAgent.updatePosition = false;
        _navAgent.updateRotation = false;

        // Инициализация цели и параметров врага
        Initialize(GameObject.FindGameObjectWithTag("Player").transform, 100, 1);

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
            AttackPlayer();
        }
        else
        {
            ChasePlayer();
        }
    }

    private void ChasePlayer()
    {
        if(target != null)
        {
            // Устанавливаем цель для преследования с помощью NavMeshAgent
            _navAgent.SetDestination(target.position);

            if(_navAgent.remainingDistance > _navAgent.stoppingDistance)
            {
                // Вычисляем направление движения
                Vector3 direction = _navAgent.desiredVelocity.normalized;

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
