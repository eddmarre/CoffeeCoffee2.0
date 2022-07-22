using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Mirror;
using UnityEngine;

public class CustomerOrderTracker : NetworkBehaviour
{
    //private Queue<Customer> _customersWaitingToOrder;
    private readonly SyncList<Customer> _customersWaitingToOrderList = new SyncList<Customer>();
    public static event Action onCustomerOrderTaken;
    public static event Action onCustomerAddedToLine;

    public static CustomerOrderTracker Instance { get; private set; }


    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }

        Instance = this;

       // _customersWaitingToOrder = new Queue<Customer>();
    }
    
    [ServerCallback]
    private void Update()
    {
        for (int i = 0; i < CustomerSpawner.Instance._customers.Count; i++)
        {
            if (!CustomerSpawner.Instance._customers[i].hasOrdered /*&&
                !_customersWaitingToOrder.Contains(CustomerSpawner.Instance._customers[i])*/
                &&!_customersWaitingToOrderList.Contains(CustomerSpawner.Instance._customers[i]))
            {
                _customersWaitingToOrderList.Add(CustomerSpawner.Instance._customers[i]);
                //_customersWaitingToOrder.Enqueue(CustomerSpawner.Instance._customers[i]);
                onCustomerAddedToLine?.Invoke();
            }
        }
    }

    public Customer CheckCurrentCustomerInFrontOfLine()
    {
        return _customersWaitingToOrderList[0];

        //return _customersWaitingToOrder.Peek();
    }

    public int GetPositionInLine(Customer customer)
    {
        // if (!_customersWaitingToOrder.Contains(customer))
        // {
        //     return -1;
        // }
        //
        // Customer[] arrayCustomers = _customersWaitingToOrder.ToArray();
        //
        // for (int i = 0; i < arrayCustomers.Length; i++)
        // {
        //     if (arrayCustomers[i] == customer)
        //         return i;
        // }
        //
        // return -1;
        if (!_customersWaitingToOrderList.Contains(customer))
        {
            return -1;
        }

        Customer[] arrayCustomers = _customersWaitingToOrderList.ToArray();
        
        for (int i = 0; i < arrayCustomers.Length; i++)
        {
            if (arrayCustomers[i] == customer)
                return i;
        }
        
        return -1;
    }

    public bool CheckIfInLine(Customer customer)
    {
        return _customersWaitingToOrderList.Contains(customer);
      //  return _customersWaitingToOrder.Contains(customer);
    }

    public void RemoveCustomerFromLine()
    {
        _customersWaitingToOrderList.Remove(_customersWaitingToOrderList[0]);
        //_customersWaitingToOrder.Dequeue();
        onCustomerOrderTaken?.Invoke();
    }
}