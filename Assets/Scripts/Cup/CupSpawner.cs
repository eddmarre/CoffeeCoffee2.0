using System;
using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;

public class CupSpawner : Interactable
{
    [SerializeField] private GameObject cupPrefab;
    [SerializeField] private Transform spawnLocation;

    [Serializable]
    private enum CupSpawnerType
    {
        Small,
        Medium,
        Large
    }

    [SerializeField] private CupSpawnerType cupSpawnerType;

    [ClientRpc]
    protected override void RpcDeactivateInteractable(Interactable interactable, Player player)
    {
        if (this != interactable) return;


        if (NetworkServer.active && !player.GetIsHoldingItem())
        {
            GameObject cupGO = Instantiate(cupPrefab, spawnLocation.position, spawnLocation.rotation);
            Cup cup = cupGO.GetComponent<Cup>();
            
            switch (cupSpawnerType)
            {
                case CupSpawnerType.Small:
                    cup.SetCupSize(Cup.CupSize.Small);
                    break;
                case CupSpawnerType.Medium:
                    cup.SetCupSize(Cup.CupSize.Medium);
                    break;
                case CupSpawnerType.Large:
                    cup.SetCupSize(Cup.CupSize.Large);
                    break;
            }
            
            cup.SetPlayer(player);

            NetworkServer.Spawn(cupGO);
        }
    }
}
