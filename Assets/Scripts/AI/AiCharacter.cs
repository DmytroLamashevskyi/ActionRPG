using CharacterMechanics;
using UnityEngine;
using UnityEngine.AI;

namespace Game.AI
{

    public interface IAIBehavior
    {
        void EnterBehavior(AiCharacter character);  // Вход в поведение
        void ExecuteBehavior(AiCharacter character);  // Выполнение логики поведения
        void ExitBehavior(AiCharacter character);  // Выход из поведения
    }

    [RequireComponent(typeof(NavMeshAgent))]
    [RequireComponent(typeof(AIController))]
    public abstract class AiCharacter : MonoBehaviour
    {

        [HideInInspector]  public NavMeshAgent navAgent;
        public Transform target;
        public Transform[] patrolPoints;
        public float detectionRange;
        public float actionRange;
        public float patruleSpeed;
        public float chaseSpeed;


        protected AIController _aiController;
        protected int _currentPatrolIndex = 0;
        protected CharacterMovement _characterMovement;
        private float _remainingDistance = 0.6f;

        void Awake()
        {
            navAgent = GetComponent<NavMeshAgent>();
            _characterMovement = GetComponent<CharacterMovement>(); 
            _aiController = GetComponent<AIController>();
            _aiController.Initialize(this);
        }

        public void SetBehavior(IAIBehavior newBehavior)
        {
            _aiController.SetBehavior(newBehavior);
        }

        public void UpdateBehavior()
        {
            _aiController.Update(); // Вызов метода Update из AIController
        }

        public bool IsTargetInDetectionRange()
        {
            return Vector3.Distance(target.position, transform.position) < detectionRange;
        }

        public bool IsTargetInActionRange()
        {
            return Vector3.Distance(target.position, transform.position) <= actionRange;
        }
        public void SetMovementSpeed(float speed)
        {
            navAgent.speed = speed;
        }

        public abstract void ActOnTarget(); // Абстрактный метод для взаимодействия с целью
        // Реализация патрулирования с проверкой на наличие _characterMovement
        public virtual void Patrol()
        {
            if(patrolPoints.Length == 0)
                return;
             
            if(navAgent.remainingDistance < _remainingDistance)
            {
                _currentPatrolIndex = (_currentPatrolIndex + 1) % patrolPoints.Length;
                navAgent.SetDestination(patrolPoints[_currentPatrolIndex].position);
            }

            var direction = navAgent.desiredVelocity.normalized;

            // Проверяем, существует ли _characterMovement перед использованием
            if(_characterMovement != null)
            {
                _characterMovement.Move(direction, false);
            }
            else
            {
                // Если _characterMovement отсутствует, используем NavMeshAgent для движения
                navAgent.Move(direction * navAgent.speed * Time.deltaTime);
            }

            Debug.Log("Враг патрулирует между точками");
        }
    }

}