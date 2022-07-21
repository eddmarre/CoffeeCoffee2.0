using System;
using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;

public class CupContents : NetworkBehaviour
{
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

    private bool _hasEspresso;
    private bool _hasMilk;

    public bool GetHasEspresso()
    {
        return _hasEspresso;
    }

    public bool GetHasMilk()
    {
        return _hasMilk;
    }

    private void Start()
    {
        _order = new Order();
    }

    private void FixedUpdate()
    {
        Debug.Log(_order.ToString());
    }

    [Command(requiresAuthority = false)]
    public void SetSize(string size)
    {
        _orderSize = size;
    }

    [Command(requiresAuthority = false)]
    public void SetShot(string shot)
    {
        _orderShot = shot;
    }

    [Command(requiresAuthority = false)]
    public void SetEspresso(string espresso)
    {
        _orderEspresso = espresso;
    }

    [Command(requiresAuthority = false)]
    public void SetSyrup(string syrup)
    {
        _orderSyrup = syrup;
    }

    [Command(requiresAuthority = false)]
    public void SetBeverage(string beverage)
    {
        _orderBeverage = beverage;
    }

    [Command(requiresAuthority = false)]
    public void SetTemperature(string temperature)
    {
        _orderTemperature = temperature;
    }

    [Command(requiresAuthority = false)]
    public void SetMilk(string milk)
    {
        _orderMilk = milk;
    }
    
    private void OnOrderSizeChange(string oldText, string newText)
    {
        _order.SetSize(newText);
    }

    private void OnOrderShotChange(string oldText, string newText)
    {
        _order.SetShots(newText);
    }

    private void OnOrderEspressoChange(string oldText, string newText)
    {
        _order.SetEspresso(newText);
        _hasEspresso = true;
    }


    private void OnOrderSyrupChange(string oldText, string newText)
    {
        _order.SetSyrup(newText);
    }


    private void OnBeverageChange(string oldText, string newText)
    {
        _order.SetBeverage(newText);
    }


    private void OnTemperatureChange(string oldText, string newText)
    {
        _order.SetTemperature(newText);
    }

    private void OnMilkChange(string oldText, string newText)
    {
        _order.SetMilk(newText);
        _hasMilk = true;
    }
}