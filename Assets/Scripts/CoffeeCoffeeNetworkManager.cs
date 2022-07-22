using System;
using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;

public class CoffeeCoffeeNetworkManager : NetworkManager
{
    private static List<Player> _players = new List<Player>();

    public static List<Player> GetPlayers()
    {
        return _players;
    }

    public override void OnServerAddPlayer(NetworkConnectionToClient conn)
    {
        base.OnServerAddPlayer(conn);

        _players.Add(conn.identity.GetComponent<Player>());


    }
    

    private void SpawnSomething(GameObject objectToSPawn)
    {
        GameObject spawnedObject = Instantiate(objectToSPawn, new Vector3(0f, 0f, -20f), Quaternion.identity);

        NetworkServer.Spawn(spawnedObject);
    }
}