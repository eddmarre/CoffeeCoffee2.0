using System;
using System.Collections;
using System.Collections.Generic;
using Mirror;
using TMPro;
using UnityEngine;

public class SyrupBottle : Interactable
{
    [SerializeField] private Animator animator;

    [SerializeField] private TextMeshProUGUI syrupTMP;

    [SyncVar(hook = nameof(OnSyrupTextChange))]
    private string _syrupText;

    public enum SyrupType
    {
        Vanilla,
        Caramel,
        Hazelnut,
        Classic,
        Mocha
    }

    [SerializeField] private SyrupType _syrupType;

    private void Start()
    {
        switch (_syrupType)
        {
            case SyrupType.Vanilla:
                _syrupText = "Vanilla";
                break;
            case SyrupType.Caramel:
                _syrupText = "Caramel";
                break;
            case SyrupType.Hazelnut:
                _syrupText = "Hazelnut";
                break;
            case SyrupType.Classic:
                _syrupText = "Classic";
                break;
            case SyrupType.Mocha:
                _syrupText = "Mocha";
                break;
        }
    }

    [ClientRpc]
    protected override void RpcDeactivateInteractable(Interactable interactable, Player player)
    {
        if (this != interactable) return;

        animator.SetTrigger("isPumped");
    }

    private void OnSyrupTextChange(string oldText, string newText)
    {
        syrupTMP.text = _syrupText;
    }
}