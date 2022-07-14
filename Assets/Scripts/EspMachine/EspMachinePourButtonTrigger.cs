using System;
using System.Collections;
using System.Collections.Generic;
using Mirror;
using Unity.VisualScripting;
using UnityEngine;

public class EspMachinePourButtonTrigger : NetworkBehaviour
{
    [SerializeField] private GameObject myPlaceableObject;
    [SerializeField] private Pickup _pickup;

    private bool _hasObject;
    
   public Pickup GetPickUp()
   {
       return _pickup;
   }

   public bool GetHasObject()
   {
       return _hasObject;
   }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.transform.TryGetComponent(out Pickup pickup)) return;

        _pickup = pickup;

       _hasObject = true;

        EspMachineButton.onPourButtonPressed += EspMachineButton_OnPourButtonPressed;
        Player.OnAnyPlayerPickUp += Player_OnAnyPlayerPickUp;
    }

    
    private void EspMachineButton_OnPourButtonPressed()
    {
        Debug.Log("filling cup");

        CmdSetVisible(false);

        EspMachineButton.onPourButtonPressed -= EspMachineButton_OnPourButtonPressed;
    }

    
    private void Player_OnAnyPlayerPickUp(Pickup item)
    {
        if (_pickup != item) return;
        
       _hasObject = false;
        _pickup = null;
        
        myPlaceableObject.GetComponent<Placeable>().SetHasItem(false);
        
        CmdSetVisible(true);

        Player.OnAnyPlayerPickUp -= Player_OnAnyPlayerPickUp;
    }

    [Command(requiresAuthority = false)]
    private void CmdSetVisible(bool value)
    {
        RpcSetVisible(value);
    }
    
    [ClientRpc]
    private void RpcSetVisible(bool value)
    {
        myPlaceableObject.SetActive(value);
    }
}