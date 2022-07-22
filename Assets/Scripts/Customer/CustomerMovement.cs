using System;
using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;
using UnityEngine.AI;

public class CustomerMovement : NetworkBehaviour
{
    [SerializeField] private NavMeshAgent navMeshAgent;
    [SerializeField] private Animator animator;
    
    private Customer _myCustomer;
    private int _positionInLine;

    private void Awake()
    {
        _myCustomer = GetComponent<Customer>();
    }

    private void OnEnable()
    {
        CustomerOrderTracker.onCustomerAddedToLine += CustomerOrderTracker_OnCustomerAddedToLine;

        CustomerOrderTracker.onCustomerOrderTaken += CustomerOrderTracker_OnCustomerOrderTaken;
    }
    

    private void OnDisable()
    {
        CustomerOrderTracker.onCustomerOrderTaken -= CustomerOrderTracker_OnCustomerOrderTaken;
        
        CustomerOrderTracker.onCustomerAddedToLine -= CustomerOrderTracker_OnCustomerAddedToLine;
    }

    private void CustomerOrderTracker_OnCustomerOrderTaken()
    {
        if (!CustomerOrderTracker.Instance.CheckIfInLine(_myCustomer)) return;

        navMeshAgent.isStopped = false;
        
        _positionInLine = CustomerOrderTracker.Instance.GetPositionInLine(_myCustomer);

        navMeshAgent.SetDestination(CustomerDestination.GetRegisterDestination(_positionInLine));
    }

    private void CustomerOrderTracker_OnCustomerAddedToLine()
    {
        _positionInLine = CustomerOrderTracker.Instance.GetPositionInLine(_myCustomer);

        navMeshAgent.SetDestination(CustomerDestination.GetRegisterDestination(_positionInLine));
    }

    private void Update()
    {
        animator.SetBool("isWalking", !navMeshAgent.isStopped);

        if (navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance)
        {
            navMeshAgent.isStopped = true;
        }
    }


    public void MoveAfterOrdering()
    {
        navMeshAgent.isStopped = false;
        navMeshAgent.SetDestination(CustomerDestination.GetDestinationAfterOrdering());
        transform.LookAt(navMeshAgent.destination);
    }

    public Vector3 GetPositionAfterStopping()
    {
        if (navMeshAgent.isStopped) return transform.position;
        return Vector3.zero;
    }
}