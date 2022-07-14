using System;
using System.Collections;
using System.Collections.Generic;
using Mirror;
using TMPro;
using UnityEngine;

public class EspMachineShotsScreen : NetworkBehaviour
{
    [SerializeField] private TextMeshPro shotSettingText;

    private char[] shotSettingTexts = {'S', 'D', 'T'};
    private int _counter;

    private void Start()
    {
        EspMachineButton.onShotsButtonPressed += CmdEspMachineButton_onShotsButtonPressed;
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