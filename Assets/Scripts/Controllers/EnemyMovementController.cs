using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyMovementController : MovementController
{
    private NavMeshAgent _agent;
    private Transform _target;


    protected override void Awake()
    {
        base.Awake();
        _agent = GetComponent<NavMeshAgent>();
        _agent.speed = speed;
        _target = GameObject.FindGameObjectWithTag("Player")?.transform;
    } 

    protected override void MoveCharacter()
    {
        if(_target == null) return; // Проверка на случай, если цель не найдена

        // Двигаемся к цели, если расстояние больше, чем остановочное
        if(Vector3.Distance(_target.position, transform.position) > _agent.stoppingDistance)
        {
            _agent.SetDestination(_target.position);
            _animatorController.SetSpeed(speed);
        }
        else
        {
            _agent.SetDestination(transform.position);
            _animatorController.SetSpeed(0);
        }

        // Получаем вектор скорости и нормализуем его
        _movementVelocity = _agent.desiredVelocity;
        base.MoveCharacter();
    }
}
