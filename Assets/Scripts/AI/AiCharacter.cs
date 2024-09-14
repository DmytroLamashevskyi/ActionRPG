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
        public float patrolWaitTime = 2f;


        protected AIController _aiController;
        public int CurrentPatrolIndex { get; protected set; } = 0;
        protected CharacterMovement _characterMovement;
        private float _remainingDistance = 0.6f;
        protected bool isWaiting = false;

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
                CurrentPatrolIndex = (CurrentPatrolIndex + 1) % patrolPoints.Length;
                navAgent.SetDestination(patrolPoints[CurrentPatrolIndex].position);
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

        internal void SetNextPatrolPoint()
        {
            if(patrolPoints.Length == 0)
                return;

            // Случайный выбор следующей точки
            int nextPatrolIndex = CurrentPatrolIndex;
            if(patrolPoints.Length > 1)
            {
                while(nextPatrolIndex == CurrentPatrolIndex)
                {
                    nextPatrolIndex = UnityEngine.Random.Range(0, patrolPoints.Length);
                }
            }

            CurrentPatrolIndex = nextPatrolIndex;
            navAgent.SetDestination(patrolPoints[CurrentPatrolIndex].position);
            Debug.Log("Следующая патрульная точка: " + CurrentPatrolIndex);
        }


        // Отрисовка патрульных точек и путей между ними в сцене
        private void OnDrawGizmosSelected()
        {
            if(patrolPoints.Length == 0)
                return;

            // Отрисовка сфер в местах патрульных точек
            Gizmos.color = Color.green;
            for(int i = 0; i < patrolPoints.Length; i++)
            {
                if(patrolPoints[i] != null)
                {
                    Gizmos.DrawSphere(patrolPoints[i].position, 0.5f); // Сфера радиусом 0.5
                }
            }

            // Отрисовка линий между патрульными точками
            Gizmos.color = Color.blue;
            for(int i = 0; i < patrolPoints.Length - 1; i++)
            {
                if(patrolPoints[i] != null && patrolPoints[i + 1] != null)
                {
                    Gizmos.DrawLine(patrolPoints[i].position, patrolPoints[i + 1].position);
                }
            }

            // Если есть замыкание маршрута
            if(patrolPoints.Length > 2 && patrolPoints[patrolPoints.Length - 1] != null && patrolPoints[0] != null)
            {
                Gizmos.DrawLine(patrolPoints[patrolPoints.Length - 1].position, patrolPoints[0].position);
            } 

            // Отрисовка радиуса обнаружения (зелёный)
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, detectionRange);

            // Отрисовка радиуса действия (оранжевый)
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, actionRange);
        }
    }

}