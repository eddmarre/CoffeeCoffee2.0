using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;

public class Customer : Interactable
{
    [SerializeField] private GameObject chatBubble;

    [ClientRpc]
    protected override void RpcDeactivateInteractable(Interactable interactable)
    {
        if (interactable != this) return;

        chatBubble.SetActive(true);
    }
}