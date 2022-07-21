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

    [SyncVar(hook = nameof(OnShotSettingChanged))]private  string _currentShotSetting;

    public  string GetCurrentSetting()
    {
        switch (_currentShotSetting)
        {
            case "S":
                return OrderDictionary.SHOTS[0];
            case "D":
                return OrderDictionary.SHOTS[1];
            case "T":
                return OrderDictionary.SHOTS[2];
            default:
                return OrderDictionary.SHOTS[0];
        }
    }

    private void Start()
    {
        _currentShotSetting = shotSettingTexts[0];
        EspMachineButton.onShotsButtonPressed += EspMachineButton_onShotsButtonPressed;
    }
    
    private void OnDisable()
    {
        EspMachineButton.onShotsButtonPressed -= EspMachineButton_onShotsButtonPressed;
    }

    private void EspMachineButton_onShotsButtonPressed()
    {
        ++_counter;
        if (_counter > shotSettingTexts.Length - 1)
            _counter = 0;

        SetShotSettingsText(shotSettingTexts[_counter]);
    }
    
    [Command(requiresAuthority = false)]
    private void SetShotSettingsText(string text)
    {
        _currentShotSetting = text;
    }

    private void OnShotSettingChanged(string oldText, string newText)
    {
        shotSettingText.text = newText;
    }
}