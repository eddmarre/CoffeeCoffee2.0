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

    private void Start()
    {
        EspMachineButton.onTemperatureButtonPressed += CmdEspMachineButton_onTemperatureButtonPressed;
    }

    private void OnDisable()
    {
        EspMachineButton.onTemperatureButtonPressed -= CmdEspMachineButton_onTemperatureButtonPressed;
    }

    [Command(requiresAuthority = false)]
    private void CmdEspMachineButton_onTemperatureButtonPressed()
    {
        RpcEspMachineButton_onTemperatureButtonPressed();
    }

    [ClientRpc]
    private void RpcEspMachineButton_onTemperatureButtonPressed()
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