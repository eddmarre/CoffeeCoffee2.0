using System;
using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;

public class MonitorShotsButton : Interactable, IMonitorOrderButton
{
    [SerializeField] private new Renderer renderer;
    [SerializeField] private new BoxCollider collider;

    [Serializable]
    private enum ShotsButton
    {
        Single,
        Double,
        Triple
    }

    [SerializeField] private ShotsButton shotsButton;


    public void SetCollision(bool value)
    {
        collider.enabled = value;
    }

    [ClientRpc]
    protected override void RpcDeactivateInteractable(Interactable interactable, Player player)
    {
        if (this != interactable) return;
        MonitorScreen.Instance.SetMonitorShotsButtonActive(this);
    }

    public void SetColor(Material color)
    {
        renderer.material = color;
    }

    public string GetButtonOrderName()
    {
        switch (shotsButton)
        {
            case ShotsButton.Single:
                return OrderDictionary.SHOTS[0];
            case ShotsButton.Double:
                return OrderDictionary.SHOTS[1];
            case ShotsButton.Triple:
                return OrderDictionary.SHOTS[2];
            default:
                return OrderDictionary.SHOTS[0];
        }
    }
}