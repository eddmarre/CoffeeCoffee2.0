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
        Ticket
    }

    [SerializeField] private PlaceableObject placeableObject;

    private bool _hasitem;

    public bool CanPlace(Pickup pickup)
    {
        switch (placeableObject)
        {
            case PlaceableObject.Cup:
                if (!pickup.TryGetComponent(out Cup cup)) return false;

                return true;
            
            case PlaceableObject.Ticket:
                if (!pickup.TryGetComponent(out Ticket ticket)) return false;

                return true;
            
            default:
                Debug.Log("can't accept this item");
                return false;
        }
    }

    public void SetHasItem(bool value)
    {
        _hasitem = value;
    }

    public bool GetHasItem()
    {
        return _hasitem;
    }

    public Vector3 GetObjectPlacementPosition()
    {
        return transform.position - new Vector3(0f, placementOffset, 0f);
    }

    public Quaternion GetObjectPlacementRotation()
    {
        return transform.rotation;
    }
}