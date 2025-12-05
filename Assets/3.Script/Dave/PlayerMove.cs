using UnityEngine;
using UnityEngine.InputSystem;
using System;
using System.Collections;
using System.Collections.Generic;

public class PlayerMove : MonoBehaviour
{
    [Header("설정")]
    public float MouseSensitivity = 0.5f; // 감도


    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void OnLook(InputValue value)
    {
        Vector2 input = value.Get<Vector2>();

        float MouseX = input.x * MouseSensitivity;
        float MouseY = input.y * MouseSensitivity;

        transform.Rotate(Vector3.up * MouseX);
        transform.Rotate(Vector3.right * -MouseY);
    }


}
