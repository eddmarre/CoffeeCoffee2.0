using System;
using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;

public class Interactable : NetworkBehaviour
{
    private void Start()
    {
        Player.OnAnyPlayerInteraction += CmdPlayer_OnAnyPlayerInteraction;
    }

    private void OnDisable()
    {
        Player.OnAnyPlayerInteraction -= CmdPlayer_OnAnyPlayerInteraction;
    }


    private void CmdPlayer_OnAnyPlayerInteraction()
    {
        RpcDeactivateInteractable();
    }

    [ClientRpc]
    private void RpcDeactivateInteractable()
    {
        Debug.Log("I've been interacted with");
        gameObject.SetActive(false);
    }
}