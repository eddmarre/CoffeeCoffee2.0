using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;

public class CoffeeCoffeeNetworkManager : NetworkManager
{
    [SerializeField] private GameObject cup;
    [SerializeField] private GameObject customer;
    private List<Player> _players = new List<Player>();

    public override void OnServerAddPlayer(NetworkConnectionToClient conn)
    {
        base.OnServerAddPlayer(conn);
        _players.Add(conn.identity.GetComponent<Player>());
        if (_players.Count > 1) return;
        
        
        SpawnSomething(cup);
       // SpawnSomething(customer);
    }

    private void SpawnSomething(GameObject objectToSPawn)
    {
        GameObject spawnedObject = Instantiate(objectToSPawn, Vector3.up, Quaternion.identity);

        NetworkServer.Spawn(spawnedObject);
    }
}