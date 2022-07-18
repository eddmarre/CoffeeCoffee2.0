using System;
using System.Collections;
using System.Collections.Generic;
using Mirror;
using TMPro;
using UnityEngine;

public class Milk : Pickup
{
    [SerializeField] private TextMeshProUGUI milkTypeTMP;
    
    [SyncVar(hook = nameof(OnMilkTextChange))]
    private string _milkText;

    [SyncVar] private string _milkOrder;

    public enum MilkType
    {
        Regular,
        Nonfat,
        Whole
    }

    private MilkType _milkType;

    public string GetMilkOrder()
    {
        return _milkOrder;
    }

    public void SetMilkType(MilkType milkType)
    {
        _milkType = milkType;
    }


    public override void OnStartClient()
    {
        SetMilkType();

        SpawnAsPlayerPickUp(this);
    }

    private void SetMilkType()
    {
        switch (_milkType)
        {
            case MilkType.Regular:
                _milkText = "R";
                _milkOrder = OrderDictionary.MILKS[0];
                break;
            case MilkType.Nonfat:
                _milkText = "N";
                _milkOrder = OrderDictionary.MILKS[1];
                break;
            case MilkType.Whole:
                _milkText = "W";
                _milkOrder = OrderDictionary.MILKS[2];
                break;
        }
    }
    
    private void OnMilkTextChange(string oldText, string newText)
    {
        milkTypeTMP.text = _milkText;
    }
}