using System;
using System.Collections;
using System.Collections.Generic;
using Mirror;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraHandler : MonoBehaviour
{
    [SerializeField] private float mouseSenesitivity = 100f;
    [SerializeField] private Transform playerTransform;

    private float _xRotation = 0f;

    private bool isTabbedout = false;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSenesitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSenesitivity * Time.deltaTime;

        _xRotation -= mouseY;
        _xRotation = Mathf.Clamp(_xRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(_xRotation, 0f, 0f);
        playerTransform.Rotate(Vector3.up * mouseX);


        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isTabbedout = !isTabbedout;
            var lockCurosr = isTabbedout
                ? Cursor.lockState = CursorLockMode.None
                : Cursor.lockState = CursorLockMode.Locked;
        }
    }

    public void SetCameraActive()
    {
        gameObject.SetActive(true);
    }

    public Camera GetPlayerCamera()
    {
        return GetComponent<Camera>();
    }
}