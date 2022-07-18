using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;

public class CupContents : NetworkBehaviour
{
    
    //TODO: Implement
    private Order _order;

    [SyncVar(hook = nameof(OnOrderSizeChange))]
    private string _orderSize;

    [SyncVar(hook = nameof(OnOrderShotChange))]
    private string _orderShot;

    [SyncVar(hook = nameof(OnOrderEspressoChange))]
    private string _orderEspresso;
    

    [SyncVar(hook = nameof(OnOrderSyrupChange))]
    private string _orderSyrup;
    

    [SyncVar(hook = nameof(OnBeverageChange))]
    private string _orderBeverage;
    
    

    [SyncVar(hook = nameof(OnTemperatureChange))]
    private string _orderTemperature;

    [SyncVar(hook = nameof(OnMilkChange))] private string _orderMilk;


    private void OnOrderSizeChange(string oldText, string newText)
    {
    }

    private void OnOrderShotChange(string oldText, string newText)
    {
    }

    private void OnOrderEspressoChange(string oldText, string newText)
    {
    }
    
    

    private void OnOrderSyrupChange(string oldText, string newText)
    {
    }
    
    

    private void OnBeverageChange(string oldText, string newText)
    {
    }

    
    
    private void OnTemperatureChange(string oldText, string newText)
    {
    }

    private void OnMilkChange(string oldText, string newText)
    {
    }
}