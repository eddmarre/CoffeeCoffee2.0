using System;
using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;

public class TicketPrinter : NetworkBehaviour
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

        if (NetworkServer.active)
        {
            GameObject newTicket =
                Instantiate(ticketGameObject, ticketGameObjectSpawn.position, ticketGameObjectSpawn.rotation);

            newTicket.GetComponent<Ticket>().SetTicketOrder(_order);

            NetworkServer.Spawn(newTicket);
        }
    }
    
}