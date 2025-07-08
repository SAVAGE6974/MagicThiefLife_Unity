using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayermoveMent : MonoBehaviour
{
    public float _moveSpeed = 5f;
    private CharacterController _cc;
    public Transform playerCamera;
    public static float mouseSensitivity;
    public float rotation;
    
    public bool isMouseLookEnabled = true;

    void Awake()
    {
        _cc = GetComponent<CharacterController>();

        mouseSensitivity = PlayerPrefs.GetFloat("MouseSensitivity", 1000f);
    }

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        // mouseSensitivity = PlayerPrefs.GetFloat("MouseSensitivity");

        Debug.Log(mouseSensitivity);
    }

    private void Update()
    {
        if (!OpenInvnetory._isOpen && !OpenSkillSelectPannel._isOpen && !OpenEscapePannel._isOpen)
        {
            PlayerMovement();
            HandleMouseLook();
        }
    }

    private void PlayerMovement()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveZ = Input.GetAxisRaw("Vertical");
         
        Vector3 movement = transform.TransformDirection(new Vector3(moveX, 0, moveZ));
        _cc.Move(movement * (_moveSpeed * Time.deltaTime));
    }

    private void HandleMouseLook()
    {
        float mouseX = Input.GetAxisRaw("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxisRaw("Mouse Y") * mouseSensitivity * Time.deltaTime;
        
        rotation -= mouseY;
        rotation = Mathf.Clamp(rotation, -80f, 80f);
        playerCamera.localRotation = Quaternion.Euler(rotation, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);
    }
}
