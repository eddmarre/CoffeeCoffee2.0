using System;
using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;

public class MonitorSyrupButton : Interactable, IMonitorOrderButton
{
    [SerializeField] private new Renderer renderer;
    [SerializeField] private new BoxCollider collider;

    [Serializable]
    private enum SyrupButton
    {
        Vanilla,
        Caramel,
        Hazelnut,
        Classic,
        Mocha
    }

    [SerializeField] private SyrupButton syrupButton;

    public void SetCollision(bool value)
    {
        collider.enabled = value;
    }

    [ClientRpc]
    protected override void RpcDeactivateInteractable(Interactable interactable)
    {
        if (this != interactable) return;
        MonitorScreen.Instance.SetMonitorSyrupButtonActive(this);
    }

    public void SetColor(Material color)
    {
        renderer.material = color;
    }

    public string GetButtonOrderName()
    {
        switch (syrupButton)
        {
            case SyrupButton.Vanilla:
                return OrderDictionary.SYRUPS[0];
            case SyrupButton.Caramel:
                return OrderDictionary.SYRUPS[1];
            case SyrupButton.Hazelnut:
                return OrderDictionary.SYRUPS[2];
            case SyrupButton.Classic:
                return OrderDictionary.SYRUPS[3];
            case SyrupButton.Mocha:
                return OrderDictionary.SYRUPS[4];
            default:
                return OrderDictionary.SYRUPS[0];
        }
    }
}