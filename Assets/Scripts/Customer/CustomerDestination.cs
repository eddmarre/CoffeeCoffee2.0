using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class CustomerDestination : MonoBehaviour
{
    [SerializeField] private Transform initialDestinationToRegister;
    [SerializeField] private Transform destinationAfterOrdering;

    private static Vector3 _registerPosition;
    private static Vector3 _afterOrderPosition;

    private void Awake()
    {
        _registerPosition = initialDestinationToRegister.position;
        _afterOrderPosition = destinationAfterOrdering.position;
    }

    public static Vector3 GetRegisterDestination(int positionInLine)
    {
        float distanceBehindCustomer = 1.5f;
        return _registerPosition + new Vector3(0f, 0f, -positionInLine * distanceBehindCustomer);
    }
    
    public static Vector3 GetDestinationAfterOrdering()
    {
        return _afterOrderPosition +
               new Vector3(Random.Range(-3f, 3f), 0f, Random.Range(-3f, 3f));
    }
    
}