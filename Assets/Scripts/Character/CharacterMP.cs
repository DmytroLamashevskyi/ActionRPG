using UnityEngine;

public class CharacterMP : MonoBehaviour
{
    public int maxMana = 100;
    private int _currentMana;

    private void Start()
    {
        _currentMana = maxMana;
    }

    // Метод для расхода маны
    public bool UseMana(int amount)
    {
        if(_currentMana >= amount)
        {
            _currentMana -= amount;
            return true;
        }
        return false;  // Недостаточно маны
    }

    // Метод для восстановления маны
    public void RecoverMana(int amount)
    {
        _currentMana += amount;
        _currentMana = Mathf.Min(_currentMana, maxMana);
    }
}
