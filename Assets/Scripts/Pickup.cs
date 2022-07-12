using System;
using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;
using UnityEngine.EventSystems;

public class Pickup : NetworkBehaviour
{
    [SerializeField] [SyncVar] private Transform _parent;
    [SerializeField] private Rigidbody rigidbody;
    private bool isBeingHeld = false;


    public bool GetIsBeingHeld()
    {
        return isBeingHeld;
    }
    
    [Server]
    public void SetParent(Transform parent)
    {
        _parent = parent;
    }
    
    public void DropItem(float throwForce)
    {
        GetComponent<NetworkIdentity>().RemoveClientAuthority();
        rigidbody.isKinematic = false;
        isBeingHeld = false;
        rigidbody.AddRelativeForce(Vector3.forward * throwForce);
    }
    
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
    
}