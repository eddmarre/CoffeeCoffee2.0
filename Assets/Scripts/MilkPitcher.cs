using System;
using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;

public class MilkPitcher : Pickup
{
    [SyncVar] private bool _isFull;
    [SyncVar] private bool _isSteamed;

    private string _currentMilkInPitcher = "none";
    private string _currentTemperatureOfMilkInPitcher = "none";

    public bool GetIsFull()
    {
        return _isFull;
    }

    public bool GetIsSteamed()
    {
        return _isSteamed;
    }

    public void PourMilkInPitcher(string milkInPitcher)
    {
        _currentMilkInPitcher = milkInPitcher;
        _isFull = true;
    }

    public void SteamMilkInPitcher(string temperatureOfMilk)
    {
        _currentTemperatureOfMilkInPitcher = temperatureOfMilk;
        _isSteamed = true;
    }

    public string GetMilkInPitcher()
    {
        return _currentMilkInPitcher;
    }

    public string GetTemperatureOfMilkInPitcher()
    {
        return _currentTemperatureOfMilkInPitcher;
    }

    private void FixedUpdate()
    {
        Debug.Log($"{_currentMilkInPitcher} {_currentTemperatureOfMilkInPitcher}");
    }

    public void PourMilkPitcherIntoCup()
    {
        _currentMilkInPitcher = "none";
        _currentTemperatureOfMilkInPitcher = "none";
        _isFull = false;
        _isSteamed = false;
    }
}