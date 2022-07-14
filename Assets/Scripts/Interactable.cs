using System;
using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;
using UnityEngine.Rendering;

public abstract class Interactable : NetworkBehaviour
{
    protected bool isActive;

    public bool GetIsActive()
    {
        return isActive;
    }

    public void SetIsActive(bool value)
    {
        isActive = value;
    }
    
    public override void OnStartServer()
    {
        Player.OnAnyPlayerInteraction += CmdPlayer_OnAnyPlayerInteraction;
    }

    public override void OnStopServer()
    {
        Player.OnAnyPlayerInteraction -= CmdPlayer_OnAnyPlayerInteraction;
    }

    [Command(requiresAuthority = false)]
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