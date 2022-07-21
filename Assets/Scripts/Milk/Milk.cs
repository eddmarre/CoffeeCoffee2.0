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

    [SyncVar] private string _milkOrderType;

    public enum MilkType
    {
        Regular,
        Nonfat,
        Whole
    }

    private MilkType _milkType;

    public string GetMilkOrderType()
    {
        return _milkOrderType;
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
                _milkOrderType = OrderDictionary.MILKS[0];
                break;
            case MilkType.Nonfat:
                _milkText = "N";
                _milkOrderType = OrderDictionary.MILKS[1];
                break;
            case MilkType.Whole:
                _milkText = "W";
                _milkOrderType = OrderDictionary.MILKS[2];
                break;
        }
    }
    
    private void OnMilkTextChange(string oldText, string newText)
    {
        milkTypeTMP.text = _milkText;
    }

    public void UseMilk()
    {
      Destroy(gameObject);
    }
}