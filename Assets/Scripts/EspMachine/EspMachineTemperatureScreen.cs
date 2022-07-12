using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EspMachineTemperatureScreen : MonoBehaviour
{
    [SerializeField] private TextMeshPro temperatureScreenText;
    
    private int _counter = 0;
    private string[] _temperatureSettings = {"Reg", "Ex Hot", "Warm"};

    private void Start()
    {
        EspMachineButton.onTemperatureButtonPressed += EspMachineButton_onTemperatureButtonPressed;
    }

    private void OnDisable()
    {
        EspMachineButton.onTemperatureButtonPressed -= EspMachineButton_onTemperatureButtonPressed;
    }


    private void EspMachineButton_onTemperatureButtonPressed()
    {
        _counter++;
        if (_counter > _temperatureSettings.Length - 1)
            _counter = 0;
        SetTemperatureText(_temperatureSettings[_counter]);
    }


    private void SetTemperatureText(string text)
    {
        temperatureScreenText.text = text;
    }
}