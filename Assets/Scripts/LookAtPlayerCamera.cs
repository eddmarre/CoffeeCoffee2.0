using System;
using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;

public class LookAtPlayerCamera : NetworkBehaviour
{
    [SerializeField] private bool invert;
    [SerializeField] private Transform targetTransform;
    [SerializeField] private List<Player> _players = new List<Player>();

    private Transform _cameraTranform;

    private void Awake()
    {
        // _cameraTranform = Camera.main.transform;
    }


    private void LateUpdate()
    {
        
        //TODO: try and figure out how to make object face each individual player
        _players = CoffeeCoffeeNetworkManager.GetPlayers();

        foreach (Player player in _players)
        {
            Debug.Log($"{player.netIdentity} has athority {player.connectionToClient.identity.hasAuthority}");
           
            if (!player.connectionToClient.identity.hasAuthority || !NetworkServer.active) return;
            
            Debug.Log("is working");

            if (invert)
            {
                Vector3 dirToCamera = (player.transform.position - targetTransform.position).normalized;
                targetTransform.LookAt(targetTransform.position + dirToCamera * -1);
            }
            else
            {
                targetTransform.LookAt(player.transform.position);
            }
        }
    }
}