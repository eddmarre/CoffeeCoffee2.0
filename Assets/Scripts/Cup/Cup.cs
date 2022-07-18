using System;
using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;

public class Cup : Pickup
{
    [SerializeField] private Transform cupTransform;
    [SerializeField] private Transform lidTransform;

    [SerializeField] private BoxCollider cupCollider;

    public enum CupSize
    {
        Small,
        Medium,
        Large
    }

    [SyncVar] private CupSize _cupSize;
    [SyncVar] private string _cupOrder;

    private Vector3 _smallCupScale;
    private Vector3 _mediumCupScale;
    private Vector3 _largeCupScale;

    private Vector3 _smallLidOffset;
    private Vector3 _mediumLidOffset;
    private Vector3 _largeLidOffset;

    private Vector3 _smallCupColliderSize;
    private Vector3 _mediumCupColliderSize;
    private Vector3 _largeCupColliderSize;

    private Vector3 _smallCupColliderCenter;
    private Vector3 _mediumCupColliderCenter;
    private Vector3 _largeCupColliderCenter;
    
    public string GetCupOrder()
    {
        return _cupOrder;
    }


    public void SetCupSize(CupSize cupSize)
    {
        _cupSize = cupSize;
    }


    public override void OnStartClient()
    {
        InitializeCupVariables();

        AssignCupSize();

        SpawnAsPlayerPickUp(this);
    }

    private void AssignCupSize()
    {
        switch (_cupSize)
        {
            case CupSize.Small:
                SetCupSize(_smallCupScale, _smallLidOffset, _smallCupColliderSize, _smallCupColliderCenter);
                _cupOrder = OrderDictionary.SIZES[0];
                break;
            case CupSize.Medium:
                SetCupSize(_mediumCupScale, _mediumLidOffset, _mediumCupColliderSize, _mediumCupColliderCenter);
                _cupOrder = OrderDictionary.SIZES[1];
                break;
            case CupSize.Large:
                SetCupSize(_largeCupScale, _largeLidOffset, _largeCupColliderSize, _largeCupColliderCenter);
                _cupOrder = OrderDictionary.SIZES[2];
                break;
        }
    }

    private void InitializeCupVariables()
    {
        _smallCupScale = new Vector3(1f, .75f, 1f);
        _mediumCupScale = new Vector3(1f, 1f, 1f);
        _largeCupScale = new Vector3(1f, 1.25f, 1f);

        Vector3 lidLocalPos = lidTransform.localPosition;

        _smallLidOffset = new Vector3(lidLocalPos.x, 0.1331f, lidLocalPos.z);
        _mediumLidOffset = new Vector3(lidLocalPos.x, lidLocalPos.y, lidLocalPos.z);
        _largeLidOffset = new Vector3(lidLocalPos.x, 0.2136f, lidLocalPos.z);

        Vector3 defaultColliderSize = cupCollider.size;

        _smallCupColliderSize = new Vector3(defaultColliderSize.x, 0.1860157f, defaultColliderSize.y);
        _mediumCupColliderSize = defaultColliderSize;
        _largeCupColliderSize = new Vector3(defaultColliderSize.x, 0.2679094f, defaultColliderSize.y);

        Vector3 defaultColliderCenter = cupCollider.center;

        _smallCupColliderCenter = new Vector3(defaultColliderCenter.x, 0.08086342f, defaultColliderCenter.z);
        _mediumCupColliderCenter = defaultColliderCenter;
        _largeCupColliderCenter = new Vector3(defaultColliderCenter.x, 0.1218103f, defaultColliderCenter.z);
    }

    private void SetCupSize(Vector3 cupSizeScale, Vector3 lidOffset, Vector3 colliderSize, Vector3 colliderCenter)
    {
        cupTransform.localScale = cupSizeScale;
        lidTransform.localPosition = lidOffset;
        cupCollider.size = colliderSize;
        cupCollider.center = colliderCenter;
    }
}