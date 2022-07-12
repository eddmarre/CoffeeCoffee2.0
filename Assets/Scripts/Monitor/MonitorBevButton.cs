using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;

public class MonitorBevButton : Interactable
{
    [SerializeField] private Renderer renderer;
    [SerializeField] private BoxCollider collider;
    public void SetCollision(bool value)
    {
        collider.enabled = value;
    }
       [ClientRpc]
    protected override void RpcDeactivateInteractable(Interactable interactable)
    {
        if(this!=interactable) return;
        MonitorScreen.Instance.SetMonitorBevButtonActive(this);
    }

    public void SetColor(Material color)
    {
        renderer.material = color;
    }
}
