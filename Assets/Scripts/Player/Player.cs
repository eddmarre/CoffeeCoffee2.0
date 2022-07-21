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
    public static event Action<Interactable, Player> OnAnyPlayerInteraction;

    public static PlayerController playerController;

    [SerializeField] private MovementHandler playerMovementHandler;
    [SerializeField] private CameraHandler playerCameraHandler;

    [SerializeField] private LayerMask pickUpLayer;
    [SerializeField] private LayerMask interactLayer;
    [SerializeField] private LayerMask placeLayer;
    [SerializeField] private LayerMask counterLayer;

    [SerializeField] private Transform holdItemTransform;

    [SyncVar] private Pickup _playerHeldPickup;

    [SerializeField] [SyncVar] private bool _isHoldingItem = false;

    private WaitForSeconds _waitForSeconds;

    public bool GetIsHoldingItem()
    {
        return _isHoldingItem;
    }

    private void Start()
    {
        // float _resetTimer = .5f;
        float _resetTimer = .1f;
        _waitForSeconds = new WaitForSeconds(_resetTimer);
    }

    #region Server

    [Command]
    public void CmdPickUp(Pickup pickupItem)
    {
        _playerHeldPickup = pickupItem;

        _playerHeldPickup.GetComponent<NetworkIdentity>().AssignClientAuthority(connectionToClient);
        _playerHeldPickup.SetParent(holdItemTransform);

        _isHoldingItem = true;
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
        _playerHeldPickup = null;
    }

    [Command]
    public void CmdInteract(Interactable interactable)
    {
        OnAnyPlayerInteraction?.Invoke(interactable, this);
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
            TryPickUpFromPlaceable();

            TryInteract();

            TryPlace();
            TryPlaceInPickup();
        }

        if (Input.GetKey(KeyCode.Q))
        {
            if (!TryPlaceOnCounter() && _isHoldingItem)
            {
                CmdDropItem();
            }
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

        if (!item.GetCanBePickedUp()) return;

        if (item.GetIsBeingHeld()) return;

        CmdPickUp(item);
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

        if (placeable.GetHasItem()) return;

        if (!placeable.CanPlace(_playerHeldPickup)) return;

        CmdPlaceItem(placeable.GetObjectPlacementPosition(), placeable.GetObjectPlacementRotation());
    }

    private void TryPlaceInPickup()
    {
        if (!_isHoldingItem) return;

        if (!Physics.Raycast(CastRay(), out RaycastHit hit, float.MaxValue, pickUpLayer)) return;

        if (hit.transform.TryGetComponent(out MilkPitcher pitcher))
        {
            if (pitcher.GetIsFull()) return;

            if (!_playerHeldPickup.TryGetComponent(out Milk milk)) return;

            pitcher.PourMilkInPitcher(milk.GetMilkOrderType());

            CmdDropItem();

            StartCoroutine(WaitUntilPlayerDropsItemBeforeDelete(milk));
        }

        if (hit.transform.TryGetComponent(out CupContents cupContents))
        {
            if (cupContents.GetHasMilk()) return;

            if (!_playerHeldPickup.TryGetComponent(out MilkPitcher milkPitcher)) return;

            if (!milkPitcher.GetIsSteamed()) return;

            cupContents.SetTemperature(milkPitcher.GetTemperatureOfMilkInPitcher());
            cupContents.SetMilk(milkPitcher.GetMilkInPitcher());

            milkPitcher.PourMilkPitcherIntoCup();
        }
    }

    private IEnumerator WaitUntilPlayerDropsItemBeforeDelete(Milk milk)
    {
        yield return _waitForSeconds;
        milk.UseMilk();
    }

    private bool TryPlaceOnCounter()
    {
        if (!_isHoldingItem) return false;

        if (!Physics.Raycast(CastRay(), out RaycastHit hit, float.MaxValue, counterLayer)) return false;

        CmdPlaceItem(hit.point, Quaternion.identity);
        return true;
    }

    private void TryPickUpFromPlaceable()
    {
        if (_isHoldingItem) return;

        if (!Physics.Raycast(CastRay(), out RaycastHit hit, float.MaxValue, placeLayer)) return;

        hit.transform.TryGetComponent(out Placeable placeable);

        if (!placeable.GetHasItem()) return;

        CmdPickUp(placeable.GetPickupInPlaceable());

        StartCoroutine(RemovePickupInPlaceableAfterWaitTime(placeable));
    }

    private IEnumerator RemovePickupInPlaceableAfterWaitTime(Placeable placeable)
    {
        yield return _waitForSeconds;
        placeable.RemovePickupInPlaceable();
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