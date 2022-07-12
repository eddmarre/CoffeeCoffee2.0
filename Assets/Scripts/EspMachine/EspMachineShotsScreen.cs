using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EspMachineShotsScreen : MonoBehaviour
{
    [SerializeField] private TextMeshPro shotSettingText;

    private char[] shotSettingTexts = {'S', 'D', 'T'};
    private int _counter;

    private void Start()
    {
        EspMachineButton.onShotsButtonPressed += EspMachineButton_onShotsButtonPressed;
    }

    private void OnDisable()
    {
        EspMachineButton.onShotsButtonPressed -= EspMachineButton_onShotsButtonPressed;
    }

    private void EspMachineButton_onShotsButtonPressed()
    {
        _counter++;
        if (_counter > shotSettingTexts.Length - 1)
            _counter = 0;
        
        SetShotSettingsText(shotSettingTexts[_counter].ToString());
    }

    private void SetShotSettingsText(string text)
    {
        shotSettingText.text = text;
    }
}