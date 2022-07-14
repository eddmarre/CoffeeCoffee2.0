using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using Mirror;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;


public class Player : NetworkBehaviour
{
    public static event Action<Interactable> OnAnyPlayerInteraction;
    public static event Action<Pickup> OnAnyPlayerPickUp;

    public static PlayerController playerController;

    [SerializeField] private MovementHandler playerMovementHandler;
    [SerializeField] private CameraHandler playerCameraHandler;

    [SerializeField] private LayerMask pickUpLayer;
    [SerializeField] private LayerMask interactLayer;
    [SerializeField] private LayerMask placeLayer;

    [SerializeField] private Transform holdItemTransform;

    [SyncVar] private Pickup _playerHeldPickup;

    [SerializeField] [SyncVar] private bool _isHoldingItem = false;


    #region Server

    [Command]
    public void CmdPickUp(Pickup pickupItem)
    {
        _playerHeldPickup = pickupItem;

        _playerHeldPickup.GetComponent<NetworkIdentity>().AssignClientAuthority(connectionToClient);
        _playerHeldPickup.SetParent(holdItemTransform);

        _isHoldingItem = true;

        OnAnyPlayerPickUp?.Invoke(pickupItem);
    }

    [Command]
    public void CmdDropItem()
    {
        _playerHeldPickup.SetParent(null);
        _playerHeldPickup.DropItem(100f);
        _isHoldingItem = false;
    }

    [Command]
    public void CmdPlaceItem(Vector3 placementPosition, Quaternion placementRotation)
    {
        _playerHeldPickup.SetParent(null);
        _playerHeldPickup.PlaceItem(placementPosition, placementRotation);
        _isHoldingItem = false;
    }

    [Command]
    public void CmdInteract(Interactable interactable)
    {
        OnAnyPlayerInteraction?.Invoke(interactable);
    }

    #endregion

    #region Client

    [ClientCallback]
    private void Update()
    {
        if (!isLocalPlayer) return;

        playerMovementHandler.MovePlayer();


        if (Input.GetKeyDown(KeyCode.E))
        {
            // if (Physics.Raycast(CastRay(), out RaycastHit hit, float.MaxValue))
            // {
            //     DebugLine(hit.point);
            //     Debug.Log($"{hit.transform.name}");
            // }

            TryPickup();
            ClientTryPickupFromEspMachine();
            TryInteract();
            TryPlace();
        }

        if (Input.GetKey(KeyCode.Q))
        {
            if (_isHoldingItem)
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
        if (_isHoldingItem)
        {
            return;
        }

        //TODO: fix range
        if (!Physics.Raycast(CastRay(), out RaycastHit hit, float.MaxValue, pickUpLayer)) return;

        hit.transform.TryGetComponent(out Pickup item);

        if (item.GetIsBeingHeld()) return;

        CmdPickUp(item);
    }

    private void ClientTryPickupFromEspMachine()
    {
        if (_isHoldingItem)
        {
            return;
        }

        if (NetworkServer.active) return;

        //TODO: fix range
        if (!Physics.Raycast(CastRay(), out RaycastHit hit, float.MaxValue)) return;

        if (!hit.transform.TryGetComponent(out EspMachinePourButtonTrigger trigger)) return;

        if (!trigger.GetHasObject()) return;

        trigger.GetPickUp();

        CmdPickUp(trigger.GetPickUp());
    }

    private void TryInteract()
    {
        //TODO: fix range
        if (!Physics.Raycast(CastRay(), out RaycastHit hit, float.MaxValue, interactLayer)) return;

        hit.transform.TryGetComponent(out Interactable interactable);

        CmdInteract(interactable);
    }

    private void TryPlace()
    {
        if (!_isHoldingItem) return;
        //TODO: fix rangeS
        if (!Physics.Raycast(CastRay(), out RaycastHit hit, float.MaxValue, placeLayer)) return;

        hit.transform.TryGetComponent(out Placeable placeable);

        if (!placeable.CanPlace(_playerHeldPickup)) return;

        if (placeable.GetHasItem()) return;

        placeable.SetHasItem(true);

        CmdPlaceItem(placeable.GetObjectPlacementPosition(), placeable.GetObjectPlacementRotation());
    }

    private Ray CastRay()
    {
        Vector2 mousePos = Mouse.current.position.ReadValue();
        Ray ray = playerCameraHandler.GetPlayerCamera().ScreenPointToRay(mousePos);

        return ray;
    }

    private void DebugLine(Vector3 position)
    {
        Debug.DrawLine(transform.position, position, Color.red, float.MaxValue);
    }
}