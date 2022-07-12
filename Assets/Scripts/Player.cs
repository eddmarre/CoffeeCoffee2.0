using System;
using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;


public class Player : NetworkBehaviour
{
    public static event Action OnAnyPlayerInteraction;
    
    public static PlayerController playerController; 
    
    [SerializeField] private MovementHandler playerMovementHandler;
    [SerializeField] private CameraHandler playerCameraHandler;
    
    [SerializeField] private LayerMask pickUpLayer;
    [SerializeField] private LayerMask interactLayer;
    
    [SerializeField] private Transform holdItemTransform;

    private Pickup _pickup;
    
    [SyncVar] private bool isHoldingItem = false;

    #region Server
    
    [Command]
    public void CmdPickUp(Pickup pickupItem)
    {
        _pickup = pickupItem;
        _pickup.GetComponent<NetworkIdentity>().AssignClientAuthority(connectionToClient);
        _pickup.SetParent(holdItemTransform);
        isHoldingItem = true;
    }

    [Command]
    public void CmdDropItem()
    {
        _pickup.SetParent(null);
        _pickup.DropItem(100f);
        isHoldingItem = false;
    }

    [Command]
    public void CmdInteract()
    {
        OnAnyPlayerInteraction?.Invoke();
    }

    #endregion

    #region Client

    
    private void Update()
    {
        if (!isLocalPlayer) return;

        playerMovementHandler.MovePlayer();

        if (Input.GetKeyDown(KeyCode.E))
        {
            TryPickup();
            TryInteract();
        }

        if (Input.GetKey(KeyCode.Q))
        {
            if (isHoldingItem)
                CmdDropItem();
        }
    }


    public override void OnStartLocalPlayer()
    {
        playerCameraHandler.SetCameraActive();
        playerController = new PlayerController();
    }

    #endregion

    private void TryPickup()
    {
        if (isHoldingItem)
        {
            return;
        }

        Vector2 mousePos = Mouse.current.position.ReadValue();
        Ray ray = playerCameraHandler.GetPlayerCamera().ScreenPointToRay(mousePos);
        //TODO: fix range
        if (!Physics.Raycast(ray, out RaycastHit hit, float.MaxValue, pickUpLayer)) return;

        hit.transform.TryGetComponent(out Pickup item);

        if (item.GetIsBeingHeld()) return;

        CmdPickUp(item);
    }

    private void TryInteract()
    {
        Vector2 mousePos = Mouse.current.position.ReadValue();
        Ray ray = playerCameraHandler.GetPlayerCamera().ScreenPointToRay(mousePos);

        if (!Physics.Raycast(ray, out RaycastHit hit, float.MaxValue, interactLayer)) return;
        
        hit.transform.TryGetComponent(out Interactable interactable);

        CmdInteract();
      
    }
}