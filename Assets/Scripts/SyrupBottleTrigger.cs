using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SyrupBottleTrigger : MonoBehaviour
{
    [SerializeField] private SyrupBottle syrupBottle;
    [SerializeField] private BoxCollider syrupBottleCollider;
    [SerializeField] private Placeable placeable;
    
    private void Start()
    {
        syrupBottle.OnSyrupPumped += SyrupBottle_OnSyrupPumped;
    }

    private void OnDestroy()
    {
        syrupBottle.OnSyrupPumped -= SyrupBottle_OnSyrupPumped;
    }

    private void FixedUpdate()
    {
        syrupBottleCollider.enabled = placeable.GetHasItem();
    }

    private void SyrupBottle_OnSyrupPumped()
    {
        if (!placeable.GetHasItem()) return;
        
        if(!placeable.GetPickupInPlaceable().TryGetComponent(out CupContents cupContents)) {Debug.Log("can't find cup contents"); return;}
        
        cupContents.SetSyrup(syrupBottle.GetSyrupText());
    }
}