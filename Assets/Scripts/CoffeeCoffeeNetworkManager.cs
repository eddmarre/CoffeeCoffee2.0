using System;
using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;

public class CoffeeCoffeeNetworkManager : NetworkManager
{
    [SerializeField] private GameObject cup;
    [SerializeField] private GameObject customer;
    private static List<Player> _players = new List<Player>();

    public static List<Player> GetPlayers()
    {
        return _players;
    }

    public override void OnServerAddPlayer(NetworkConnectionToClient conn)
    {
        base.OnServerAddPlayer(conn);
        
        _players.Add(conn.identity.GetComponent<Player>());
        
        
        if (_players.Count > 1) return;


        SpawnSomething(customer);
        // SpawnSomething(cup);
        // SpawnSomething(cup);
    }

    private void SpawnSomething(GameObject objectToSPawn)
    {
        GameObject spawnedObject = Instantiate(objectToSPawn, Vector3.up, Quaternion.identity);

        NetworkServer.Spawn(spawnedObject);
    }
}