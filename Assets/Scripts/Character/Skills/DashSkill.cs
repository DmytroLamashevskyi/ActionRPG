using System.Collections;
using UnityEngine;

public class DashSkill : MonoBehaviour
{
    public float dashDuration = 0.2f; // Длительность рывка
    public float dashCooldown = 1.0f; // Время восстановления рывка
    private bool _isDashing = false;   // Проверка на рывок
    private bool _canDash = true;      // Проверка, можно ли выполнить рывок
    private CharacterController _characterController;

    void Start()
    {
        _characterController = GetComponent<CharacterController>();
    }

    public void PerformDash(Vector3 direction, float dashDistance)
    {
        if(!_canDash) return; // Проверка, можно ли выполнить рывок

        StartCoroutine(DashCoroutine(direction, dashDistance));
    }

    private IEnumerator DashCoroutine(Vector3 direction, float dashDistance)
    {
        _isDashing = true;
        _canDash = false;

        float dashSpeed = dashDistance / dashDuration; // Рассчитываем скорость рывка
        float elapsedTime = 0f;

        while(elapsedTime < dashDuration)
        {
            Vector3 dashVector = direction * dashSpeed * Time.deltaTime;
            _characterController.Move(dashVector); // Плавное перемещение
            elapsedTime += Time.deltaTime;

            yield return null;
        }

        _isDashing = false;

        yield return new WaitForSeconds(dashCooldown);
        _canDash = true;
    }

    public bool CanDash()
    {
        return _canDash;
    }

    public bool IsDashing()
    {
        return _isDashing;
    }
}
