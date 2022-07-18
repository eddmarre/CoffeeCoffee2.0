using System;
using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;

public class MilkSpawner : Interactable
{
    [SerializeField] private GameObject milkPrefab;
    [SerializeField] private Transform spawnLocation;

    [Serializable]
    private enum MilkSpawnerType
    {
        Regular,
        Nonfat,
        Whole
    }

    [SerializeField] private MilkSpawnerType milkSpawnerType;

    [ClientRpc]
    protected override void RpcDeactivateInteractable(Interactable interactable, Player player)
    {
        if (this != interactable) return;


        if (NetworkServer.active && !player.GetIsHoldingItem())
        {
            GameObject milkGO = Instantiate(milkPrefab, spawnLocation.position, spawnLocation.rotation);
            Milk milk = milkGO.GetComponent<Milk>();
            
            switch (milkSpawnerType)
            {
                case MilkSpawnerType.Regular:
                    milk.SetMilkType(Milk.MilkType.Regular);
                    break;
                case MilkSpawnerType.Nonfat:
                    milk.SetMilkType(Milk.MilkType.Nonfat);
                    break;
                case MilkSpawnerType.Whole:
                    milk.SetMilkType(Milk.MilkType.Whole);
                    break;
            }
            
            milk.SetPlayer(player);

            NetworkServer.Spawn(milkGO);
        }
    }
}