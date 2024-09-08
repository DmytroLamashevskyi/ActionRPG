using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public float horizontalInput;
    public float verticalInput;
    public bool jumpInput;
    public bool speedUp;

    void Update()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
        jumpInput = Input.GetButtonDown("Jump");
        speedUp = Input.GetKey(KeyCode.LeftShift);
    }

    private void OnDisable()
    {
        horizontalInput = 0f;
        verticalInput = 0f; 
    }
}
