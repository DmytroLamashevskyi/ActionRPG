using UnityEngine;

public class CharacterHP : MonoBehaviour
{
    public int maxHealth = 100;
    private int _currentHealth;

    private void Start()
    {
        _currentHealth = maxHealth;
    }

    // Метод для получения урона
    public void TakeDamage(int damage)
    {
        _currentHealth -= damage;
        if(_currentHealth <= 0)
        {
            Die();
        }
    }

    // Метод для восстановления здоровья
    public void Heal(int amount)
    {
        _currentHealth += amount;
        _currentHealth = Mathf.Min(_currentHealth, maxHealth);  // Ограничиваем здоровье максимальным значением
    }

    private void Die()
    {
        // Логика смерти персонажа
        Debug.Log(gameObject.name + " погиб");
    }
}
