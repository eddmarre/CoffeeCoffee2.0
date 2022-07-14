using System;
using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;

public class MonitorTemperatureButton : Interactable, IMonitorOrderButton
{
    [SerializeField] private new Renderer renderer;
    [SerializeField] private new BoxCollider collider;

    [Serializable]
    private enum TemperatureButton
    {
        Regular,
        Warm,
        ExtraHot
    }

    [SerializeField] private TemperatureButton temperatureButton;
    
    public void SetCollision(bool value)
    {
        collider.enabled = value;
    }
    
    [ClientRpc]
    protected override void RpcDeactivateInteractable(Interactable interactable)
    {
        if (this != interactable) return;
        MonitorScreen.Instance.SetMonitorTemperatureButtonActive(this);
    }

    public void SetColor(Material color)
    {
        renderer.material = color;
    }

    public string GetButtonOrderName()
    {
        switch (temperatureButton)
        {
            case TemperatureButton.Regular:
                return OrderDictionary.TEMPERATURES[0];
            case TemperatureButton.Warm:
                return OrderDictionary.TEMPERATURES[1];
            case TemperatureButton.ExtraHot:
                return OrderDictionary.TEMPERATURES[2];
            default:
                return OrderDictionary.TEMPERATURES[0];
        }
    }
}