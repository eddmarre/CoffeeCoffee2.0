using System;
using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;

public class MonitorOrderBevButton : Interactable, IMonitorOrderButton
{
    [SerializeField] private new Renderer renderer;
    [SerializeField] private new BoxCollider collider;

    [Serializable]
    private enum BevButton
    {
        Latte,
        Machiato
    }

    [SerializeField] private BevButton bevButton;

    public void SetCollision(bool value)
    {
        collider.enabled = value;
    }

    [ClientRpc]
    protected override void RpcDeactivateInteractable(Interactable interactable)
    {
        if (this != interactable) return;
        MonitorScreen.Instance.SetMonitorBevButtonActive(this);
    }

    public void SetColor(Material color)
    {
        renderer.material = color;
    }

    public string GetButtonOrderName()
    {
        switch (bevButton)
        {
            case BevButton.Latte:
                return OrderDictionary.BEVERAGES[0];
            case BevButton.Machiato:
                return OrderDictionary.BEVERAGES[1];
            default:
                return OrderDictionary.BEVERAGES[0];
        }
    }
}