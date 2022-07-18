using System;
using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;

public class MonitorMilkButton : Interactable, IMonitorOrderButton
{
    [SerializeField] private new Renderer renderer;
    [SerializeField] private new BoxCollider collider;

    [Serializable]
    private enum MilkButton
    {
        Regular,
        Nonfat,
        Whole
    }

    [SerializeField] private MilkButton milkButton;
    public void SetCollision(bool value)
    {
        collider.enabled = value;
    }

    [ClientRpc]
    protected override void RpcDeactivateInteractable(Interactable interactable, Player player)
    {
        if (this != interactable) return;

        MonitorScreen.Instance.SetMonitorMilkButtonActive(this);
    }

    public void SetColor(Material color)
    {
        renderer.material = color;
    }

    public string GetButtonOrderName()
    {
        switch (milkButton)
        {
            case MilkButton.Regular:
                return OrderDictionary.MILKS[0];
            case MilkButton.Nonfat:
                return OrderDictionary.MILKS[1];
            case MilkButton.Whole:
                return OrderDictionary.MILKS[2];
            default:
                return OrderDictionary.MILKS[0];
        }
    }
}