using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EspMachineLeverController : MonoBehaviour
{
    [SerializeField] private Transform leverRotationTransform;
    [SerializeField] private MilkSteamTrigger milkSteamTrigger;

    private bool _isLeverDown;

    private Quaternion _moveToRot;
    
    private float _leverMoveSpeed = 3f;

    private void Start()
    {
        _moveToRot = Quaternion.Euler(0f, 0f, 0f);

        EspMachineButton.onLeverButtonPressed += EspMachineButton_OnLeverButtonPressed;
    }

    private void OnDestroy()
    {
        EspMachineButton.onLeverButtonPressed += EspMachineButton_OnLeverButtonPressed;
    }

    private void EspMachineButton_OnLeverButtonPressed()
    {
        if (milkSteamTrigger.GetIsSteaming()) return;
        
        _isLeverDown = !_isLeverDown;

        _moveToRot = Quaternion.Euler(0f, 0f, _isLeverDown ? 90f : 0f);
    }


    private void Update()
    {
        leverRotationTransform.rotation =
            Quaternion.Slerp(leverRotationTransform.rotation, _moveToRot,
                _leverMoveSpeed * Time.deltaTime);
    }

    public bool GetIsLeverDown()
    {
        return _isLeverDown;
    }
}