using CharacterMechanics;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    private CharacterMovement _characterMovement;

    public float walkSpeed = 2f;
    public float runSpeed = 6f;

    private void Start()
    {
        _characterMovement = GetComponent<CharacterMovement>();
    }

    private void Update()
    {
        HandleMovement();
        HandleJump();
    }

    private void HandleMovement()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        Vector3 moveDirection = new Vector3(moveX, 0, moveZ).normalized;

        if(moveDirection.magnitude > 0)
        {
            bool isRunning = Input.GetKey(KeyCode.LeftShift);
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
}
