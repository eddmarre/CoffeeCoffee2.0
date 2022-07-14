using System;
using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;

public class TicketPrinter : Interactable
{
    private Order _order;
    [SerializeField] private GameObject ticketGameObject;
    [SerializeField] private Transform ticketGameObjectSpawn;

    private void Start()
    {
        MonitorScreen.Instance.OnOrderCreated += MonitorScreen_OnOrderCreated;
    }

    private void OnDestroy()
    {
        MonitorScreen.Instance.OnOrderCreated -= MonitorScreen_OnOrderCreated;
    }


    private void MonitorScreen_OnOrderCreated(Order order)
    {
        _order = order;
    }

    [ClientRpc]
    protected override void RpcDeactivateInteractable(Interactable interactable)
    {
        if (this != interactable) return;

        if (_order == null) return;

        //Debug.Log(_order.ToString());
      
        if (NetworkServer.active)
        {
            GameObject newTicket =
                Instantiate(ticketGameObject, ticketGameObjectSpawn.position, ticketGameObjectSpawn.rotation);

            newTicket.GetComponent<Ticket>().SetOrder(_order);
            
            NetworkServer.Spawn(newTicket);
        }

        _order = null;
    }
}