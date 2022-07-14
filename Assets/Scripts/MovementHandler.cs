using System;
using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;

public class MovementHandler : NetworkBehaviour
{
    [SerializeField] private Animator animator;
    
    private Vector2 _previousInput;

    private void Start()
    {
        Player.playerController.PlayerControls.Move.performed += (ctx) => _previousInput = ctx.ReadValue<Vector2>();
        Player.playerController.PlayerControls.Move.canceled += (ctx) => _previousInput = ctx.ReadValue<Vector2>();
        Player.playerController.PlayerControls.Enable();
    }

    public void MovePlayer()
    {
        Vector3 currentPos = transform.position;

        Vector3 inputDir = new Vector3(_previousInput.x, 0f, _previousInput.y);

        //ensure we move forward relative to camera direction
        Vector3 moveDir = inputDir.z * transform.forward + inputDir.x * transform.right;

        float moveSpeed = 10f;
        transform.position += moveDir * moveSpeed * Time.deltaTime;

        CmdSetWalkingAnimation(currentPos);
    }

    [Command]
    private void CmdSetWalkingAnimation(Vector3 currentPos)
    {
        RpcSetWalkingAnimation(currentPos);
    }
    [ClientRpc]
    private void RpcSetWalkingAnimation(Vector3 currentPos)
    {
        if (currentPos != transform.position)
        {
            animator.SetBool("isWalking", true);
        }
        else
        {
            animator.SetBool("isWalking", false);
        }
    }
}