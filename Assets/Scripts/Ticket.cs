using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Ticket : Pickup
{
    private Order _order;
    [SerializeField] private TextMeshProUGUI _text;

    public void SetOrder(Order order)
    {
        _order = order;

        _text.text =
            $"size: {_order.GetSize()}\nshot: {_order.GetShot()}\nEspresso: {_order.GetEspresso()}\nSyrup: {_order.GetSyrup()}\nBeverage: {_order.GetBeverage()}\nTemperature: {_order.GetTemperature()}\nMilk: {_order.GetMilk()}";
    }
}