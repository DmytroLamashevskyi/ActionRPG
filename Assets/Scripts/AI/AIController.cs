using UnityEngine;

namespace Game.AI
{

    public class AIController : MonoBehaviour
    {
        private IAIBehavior _currentBehavior;
        private AiCharacter _aiCharacter;

        public void Initialize(AiCharacter aiCharacter)
        {
            this._aiCharacter = aiCharacter;
        }

        public void SetBehavior(IAIBehavior newBehavior)
        {
            _currentBehavior?.ExitBehavior(_aiCharacter); // Выход из текущего поведения
            _currentBehavior = newBehavior;
            _currentBehavior?.EnterBehavior(_aiCharacter); // Вход в новое поведение
        }

        public void Update()
        {
            _currentBehavior?.ExecuteBehavior(_aiCharacter); // Правильное выполнение текущего поведения
        }
    }
}
