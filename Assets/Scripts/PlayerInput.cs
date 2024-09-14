using CharacterMechanics;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    private CharacterMovement _characterMovement;
    private CharacterAttack _characterAttack;
    private DashSkill _dashSkill;

    public float dashDistance = 5f;  // Дистанция рывка
    public float doubleShiftTime = 0.3f;  // Максимальный промежуток между двойным нажатием Shift

    private float _lastShiftTime = -1f;  // Время последнего нажатия Shift
    private bool _isAttacking;

    private void Start()
    {
        _characterMovement = GetComponent<CharacterMovement>();
        _characterAttack = GetComponent<CharacterAttack>();
        _dashSkill = GetComponent<DashSkill>();  // Добавляем компонент DashSkill
    }

    private void Update()
    {
        if(!_isAttacking)  // Разрешаем движение и прыжок только если не атакуем
        {
            HandleJump();
            HandleDash();
        }

        HandleMovement();
        HandleAttack();
    }

    private void HandleMovement()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        Vector3 moveDirection = new Vector3(moveX, 0, moveZ).normalized;

        if(moveDirection.magnitude > 0)
        {
            bool isRunning = Input.GetKey(KeyCode.LeftShift) && !_isAttacking;
            _characterMovement.Move(moveDirection, isRunning);
        }
        else
        {
            _characterMovement.Stop();
        }
    }

    private void HandleJump()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            _characterMovement.Jump();  // Обрабатываем прыжок
        }
    }

    private void HandleDash()
    {
        // Проверяем, было ли нажатие Shift
        if(Input.GetKeyDown(KeyCode.LeftShift))
        {
            float currentTime = Time.time;

            // Если время между нажатиями Shift меньше doubleShiftTime, выполняем рывок
            if(currentTime - _lastShiftTime <= doubleShiftTime && _dashSkill.CanDash())
            {
                PerformDash();
            }

            // Обновляем время последнего нажатия Shift
            _lastShiftTime = currentTime;
        }
    }

    private void PerformDash()
    {
        // Выполняем рывок в направлении движения
        Vector3 dashDirection = _characterMovement.GetMoveDirection();
        _dashSkill.PerformDash(dashDirection, dashDistance);
    }

    private void HandleAttack()
    {
        // Если нажата левая кнопка мыши - выполняем атаку
        if(Input.GetMouseButtonDown(0))  // ЛКМ для атаки
        {
            _isAttacking = true;  // Включаем состояние атаки
            _characterAttack.Attack();  // Запускаем анимацию атаки

            float attackDuration = _characterAttack.GetAttackSpeed();
            Invoke("EndAttack", attackDuration);
        }
    }

    private void EndAttack()
    {
        _isAttacking = false;  // Разрешаем снова двигаться
        _characterAttack.EndAttack();  // Завершаем анимацию атаки
    }
}
