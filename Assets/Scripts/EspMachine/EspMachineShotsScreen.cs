using System;
using System.Collections;
using System.Collections.Generic;
using Mirror;
using TMPro;
using UnityEngine;

public class EspMachineShotsScreen : NetworkBehaviour
{
    [SerializeField] private TextMeshPro shotSettingText;

    private string[] shotSettingTexts = {"S", "D", "T"};
    private int _counter;

    private  string _currentSetting;

    public  string GetCurrentSetting()
    {
        return _currentSetting;
    }

    private void Start()
    {
        _currentSetting = OrderDictionary.SHOTS[0];
        EspMachineButton.onShotsButtonPressed += CmdEspMachineButton_onShotsButtonPressed;
    }

    private void Update()
    {
        //not sure why but this is the order that correctly displays shot information (should be in order)
        switch (_counter)
        {
            case 0:
                _currentSetting = OrderDictionary.SHOTS[1];
                break;
            case 1:
                _currentSetting = OrderDictionary.SHOTS[2];
                break;
            case 2:
                _currentSetting = OrderDictionary.SHOTS[0];
                break;
            default:
                _currentSetting = OrderDictionary.SHOTS[0];
                break;
        }
    }

    private void OnDisable()
    {
        EspMachineButton.onShotsButtonPressed -= CmdEspMachineButton_onShotsButtonPressed;
    }

    [Command(requiresAuthority = false)]
    private void CmdEspMachineButton_onShotsButtonPressed()
    {
        RpcEspMachineButton_onShotsButtonPressed();
    }

    [ClientRpc]
    private void RpcEspMachineButton_onShotsButtonPressed()
    {
        ++_counter;
        if (_counter > shotSettingTexts.Length - 1)
            _counter = 0;

        SetShotSettingsText(shotSettingTexts[_counter]);
       
    }

    private void SetShotSettingsText(string text)
    {
        shotSettingText.text = text;
    }
}