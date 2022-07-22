using System;
using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;

public class CustomerSpawner : NetworkBehaviour
{
    private float _timer;
    private float _timeToSpawnNextCustomer = 3f;

    public readonly SyncList<Customer> _customers = new SyncList<Customer>();

    private int _maxCustomers = 5;

    public static CustomerSpawner Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }

        Instance = this;
    }

    [ServerCallback]
    private void Start()
    {
        SpawnCustomer();
        Customer.onCustomerServed += Customer_OnCustomerServed;
    }

    private void OnDestroy()
    {
        Customer.onCustomerServed -= Customer_OnCustomerServed;
    }

    private void Customer_OnCustomerServed(Customer customer)
    {
        DeleteCustomer(customer);
    }

    private void SpawnCustomer()
    {
        GameObject customer = PrefabPool.singleton.Get(new Vector3(0f, 0f, -20f), Quaternion.identity);

        _customers.Add(customer.GetComponent<Customer>());

        NetworkServer.Spawn(customer);
    }

    [ServerCallback]
    private void Update()
    {
        if (_customers.Count >= _maxCustomers) return;

        _timer += Time.deltaTime;

        if (_timer >= _timeToSpawnNextCustomer)
        {
            SpawnCustomer();
            _timer = 0f;
        }
    }
    
    private void DeleteCustomer(Customer customer)
    {
        _customers.Remove(customer);

        NetworkServer.UnSpawn(customer.gameObject);

        PrefabPool.singleton.Return(customer.gameObject);
    }
}