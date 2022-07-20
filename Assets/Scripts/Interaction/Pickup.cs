using System;
using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class Pickup : NetworkBehaviour
{
    [SerializeField] [SyncVar] private Transform _parent;

    [SerializeField] private new Rigidbody rigidbody;

    [SerializeField] private bool isBeingHeld = false;


    [SyncVar] private Player _player;

    private bool canBePickedUp = true;

    public void SetCanBePickedUp(bool value)
    {
        canBePickedUp = value;
    }

    public bool GetCanBePickedUp()
    {
        return canBePickedUp;
    }

    public bool GetIsBeingHeld()
    {
        return isBeingHeld;
    }

    public void SetPlayer(Player player)
    {
        _player = player;
    }

    [Server]
    public void SetParent(Transform parent)
    {
        _parent = parent;
    }

    [Server]
    public void DropItem(float throwForce)
    {
        GetComponent<NetworkIdentity>().RemoveClientAuthority();
        rigidbody.isKinematic = false;
        isBeingHeld = false;
        rigidbody.AddRelativeForce(Vector3.forward * throwForce);
    }

    [Server]
    public void PlaceItem(Vector3 placePosition, Quaternion placeRotation)
    {
        GetComponent<NetworkIdentity>().RemoveClientAuthority();
        isBeingHeld = false;
        transform.position = placePosition;
        transform.rotation = placeRotation;
    }

    [ServerCallback]
    private void Update()
    {
        if (_parent != null)
        {
            transform.position = _parent.position;
            transform.rotation = _parent.rotation;
            rigidbody.isKinematic = true;
            isBeingHeld = true;
        }
    }

    protected void SpawnAsPlayerPickUp(Pickup pickup)
    {
        _player.CmdPickUp(pickup);
        _player = null;
    }
    
}