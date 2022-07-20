using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EspMachineLeverController : MonoBehaviour
{
    [SerializeField] private Transform _leverRotationTransform;


    private bool _isLeverDown;

    private Quaternion originalRot;

    private void Start()
    {
        originalRot = _leverRotationTransform.rotation;
        EspMachineButton.onLeverButtonPressed += EspMachineButton_OnLeverButtonPressed;
    }

    private void OnDestroy()
    {
        EspMachineButton.onLeverButtonPressed += EspMachineButton_OnLeverButtonPressed;
    }

    private void EspMachineButton_OnLeverButtonPressed()
    {
        _isLeverDown = !_isLeverDown;

        if (_isLeverDown)
        {
        }
    }


    private void Update()
    {
        if (!_isLeverDown) return;
        _leverRotationTransform.rotation =
            Quaternion.RotateTowards(_leverRotationTransform.rotation, new Quaternion(0f, 0f, 80f, 0f),
                1f * Time.deltaTime);
    }
}