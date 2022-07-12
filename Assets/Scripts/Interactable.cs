using System;
using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;

public abstract class Interactable : NetworkBehaviour
{
    public override void OnStartServer()
    {
        Player.OnAnyPlayerInteraction += CmdPlayer_OnAnyPlayerInteraction;
    }

    public override void OnStopServer()
    {
        Player.OnAnyPlayerInteraction -= CmdPlayer_OnAnyPlayerInteraction;
    }


    private void CmdPlayer_OnAnyPlayerInteraction(Interactable interactable)
    {
        RpcDeactivateInteractable(interactable);
    }

    [ClientRpc]
    protected virtual void RpcDeactivateInteractable(Interactable interactable)
    {
        Debug.Log($"I've been presed {interactable.transform.name}");
    }
}