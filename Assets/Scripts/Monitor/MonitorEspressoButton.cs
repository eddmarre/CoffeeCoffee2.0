using System;
using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;

public class MonitorEspressoButton : Interactable, IMonitorOrderButton
{
    [SerializeField] private new Renderer renderer;
    [SerializeField] private new BoxCollider collider;

    [Serializable]
    private enum EspressoButton
    {
        Regular,
        Blonde,
        Decaf
    }

    [SerializeField] private EspressoButton espressoButton;
    

    public void SetCollision(bool value)
    {
        collider.enabled = value;
    }
    [ClientRpc]
    protected override void RpcDeactivateInteractable(Interactable interactable, Player player)
    {
        if(this!=interactable) return;
        
        MonitorScreen.Instance.SetMonitorEspressoButtonActive(this);
    }

    public void SetColor(Material color)
    {
        renderer.material = color;
    }

    public string GetButtonOrderName()
    {
        switch (espressoButton)
        {
            case EspressoButton.Regular:
                return OrderDictionary.ESPRESSOS[0];
            case EspressoButton.Blonde:
                return OrderDictionary.ESPRESSOS[1];
            case EspressoButton.Decaf:
                return OrderDictionary.ESPRESSOS[2];
            default:
                return OrderDictionary.ESPRESSOS[0];
        }
    }
}
