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
        public NavMeshAgent navAgent;
        public Transform target; // Цель может быть врагом или союзником
        public float detectionRange;
        public float actionRange;

        private AIController _aiController;

        public void Initialize(Transform target, float detectionRange, float actionRange)
        {
            this.target = target;
            this.detectionRange = detectionRange;
            this.actionRange = actionRange;
            navAgent = GetComponent<NavMeshAgent>();

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

        public abstract void ActOnTarget(); // Абстрактный метод для взаимодействия с целью

        public virtual void Patrol()
        {
            Debug.Log("Патрулирование...");
        }
    }

}