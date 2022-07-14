using System;
using System.Collections;
using System.Collections.Generic;
using Mirror;
using Monitor;
using UnityEngine;

public class MonitorPageButton : Interactable
{
    [SerializeField] private new Renderer renderer;
    [SerializeField] private GameObject pageScreen;
    [SerializeField] private Transform spawnedTransform;


    [ClientRpc]
    protected override void RpcDeactivateInteractable(Interactable interactable)
    {
        if (this != interactable) return;
        
        MonitorScreen.Instance.SetPageButtonActive(this);
    }

    public void SetMaterial(Material color)
    {
        renderer.material = color;
    }

    public void SetPageScreenVisible(bool value)
    {
        pageScreen.transform.localPosition = value ? Vector3.zero : new Vector3(0f, 0f, -0.005f);

        if (pageScreen.TryGetComponent(out MonitorEspressoPage espressoPage))
        {
            espressoPage.SetCollisions(value);
        }

        if (pageScreen.TryGetComponent(out MonitorSyrupPage syrupPage))
        {
            syrupPage.SetCollisions(value);
        }
        
        if(pageScreen.TryGetComponent(out MonitorBevPage bevPage))
        {
            bevPage.SetCollisions(value);
        }

        if (pageScreen.TryGetComponent(out MonitorMilkPage milkPage))
        {
            milkPage.SetCollisions(value);
        }
        
       
    }
}