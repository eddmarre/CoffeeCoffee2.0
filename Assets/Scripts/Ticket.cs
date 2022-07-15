using System;
using System.Collections;
using System.Collections.Generic;
using Mirror;
using TMPro;
using UnityEngine;

public class Ticket : Pickup
{
    [SerializeField] private TextMeshProUGUI _text;

    //ensures that everyone sees ticket order
    [SyncVar(hook = nameof(OnOrderTextChange))]
    private string _orderText;

    private Order _order;
    
    public void SetTicketOrder(Order order)
    {
        _order = order;
        _orderText =
            $"size: {_order.GetSize()}\n" +
            $"shot: {_order.GetShot()}\n" +
            $"Espresso: {_order.GetEspresso()}\n" +
            $"Syrup: {_order.GetSyrup()}\n" +
            $"Beverage: {_order.GetBeverage()}\n" +
            $"Temperature: {_order.GetTemperature()}\n" +
            $"Milk: {_order.GetMilk()}";
    }

    private void OnOrderTextChange(string oldText, string newText)
    {
        _text.text = newText;
    }
}