using System;
using System.Collections;
using System.Collections.Generic;
using Mirror;
using TMPro;
using UnityEngine;

public class EspMachineTemperatureScreen : NetworkBehaviour
{
    [SerializeField] private TextMeshPro temperatureScreenText;

    private int _counter = 0;
    private string[] _temperatureSettings = {"Reg", "Ex Hot", "Warm"};

    [SyncVar(hook = nameof(OnCurrentTemperatureSettingChanged))]
    private string _currentTemperatureSetting;

    private void Awake()
    {
        _currentTemperatureSetting = _temperatureSettings[0];
    }

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

    [Command(requiresAuthority = false)]
    private void SetTemperatureText(string text)
    {
        _currentTemperatureSetting = text;
    }

    public string GetCurrentTemperatureSetting()
    {
        switch (_currentTemperatureSetting)
        {
            case "Reg":
                return OrderDictionary.TEMPERATURES[0];
            case "Warm":
                return OrderDictionary.TEMPERATURES[1];
            case "Ex Hot":
                return OrderDictionary.TEMPERATURES[2];
            default:
                return OrderDictionary.TEMPERATURES[0];
        }
    }

    private void OnCurrentTemperatureSettingChanged(string oldText, string newText)
    {
        temperatureScreenText.text = newText;
    }
}