using System;
using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;

public class Placeable : MonoBehaviour
{
    [SerializeField] private float placementOffset = .11f;


    [Serializable]
    private enum PlaceableObject
    {
        Cup,
        Ticket,
        Pitcher
    }

    [SerializeField] private PlaceableObject placeableObject;

    private bool _hasItem;

    private Pickup _pickupInPlaceable;

    public Pickup GetPickupInPlaceable()
    {
        return _pickupInPlaceable;
    }

    public void SetHasItem(bool value)
    {
        _hasItem = value;
    }

    public bool GetHasItem()
    {
        return _hasItem;
    }

    public Vector3 GetObjectPlacementPosition()
    {
        return transform.position - new Vector3(0f, placementOffset, 0f);
    }

    public Quaternion GetObjectPlacementRotation()
    {
        return transform.rotation;
    }

    public bool CanPlace(Pickup pickup)
    {
        switch (placeableObject)
        {
            case PlaceableObject.Cup:
                if (!pickup.TryGetComponent(out Cup cup)) return false;
                PlacePickupInPlaceable(pickup);
                return true;

            case PlaceableObject.Ticket:
                if (!pickup.TryGetComponent(out Ticket ticket)) return false;
                PlacePickupInPlaceable(pickup);
                return true;
            
            case PlaceableObject.Pitcher:
                if (!pickup.TryGetComponent(out MilkPitcher pitcher)) return false;
                PlacePickupInPlaceable(pickup);
                return true;

            default:
                Debug.Log("can't accept this item");
                return false;
        }
    }

    public void RemovePickupInPlaceable()
    {
        _pickupInPlaceable.SetCanBePickedUp(true);
        _hasItem = false;
        _pickupInPlaceable = null;
    }

    private void PlacePickupInPlaceable(Pickup pickup)
    {
        _pickupInPlaceable = pickup;
        _hasItem = true;
        pickup.SetCanBePickedUp(false);
    }
}