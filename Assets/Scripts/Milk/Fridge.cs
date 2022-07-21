using System;
using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;

public class Fridge : Interactable
{
    [SerializeField] private Animator animator;

    [SerializeField] private new BoxCollider collider;

    private Vector3 _closeColliderCenter;
    private Vector3 _closeColliderSize;

    private Vector3 _openColliderCenter;
    private Vector3 _openColliderSize;


    private void Start()
    {
        _closeColliderCenter = new Vector3(-0.00436902f, 1.665962f, 0.01935685f);
        _closeColliderSize = new Vector3(0.799026f, 1.21689f, 0.7599757f);

        _openColliderCenter = new Vector3(-0.4f, 1.665962f, 0.734831f);
        _openColliderSize = new Vector3(0.09696054f, 1.21689f, 0.7523143f);

        SetColliderSize(_closeColliderCenter, _closeColliderSize);
    }


    [ClientRpc]
    protected override void RpcDeactivateInteractable(Interactable interactable, Player player)
    {
        if (this != interactable) return;

        SetIsActive(!GetIsActive());

        animator.SetBool("isOpen", GetIsActive());

        if (GetIsActive())
        {
            SetColliderSize(_openColliderCenter, _openColliderSize);
        }
        else
        {
            SetColliderSize(_closeColliderCenter, _closeColliderSize);
        }
    }

    private void SetColliderSize(Vector3 center, Vector3 size)
    {
        collider.center = center;
        collider.size = size;
    }
    
    
    public void CloseFridge()
    {
        SetIsActive(false);
        animator.SetBool("isOpen", GetIsActive());
        SetColliderSize(_closeColliderCenter, _closeColliderSize);
    }
}