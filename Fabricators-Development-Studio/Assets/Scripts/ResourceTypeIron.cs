using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceTypeIron : MonoBehaviour
{
    [HideInInspector] public int _resourceAmount;
    
    void Awake()
    {
        _resourceAmount = Random.Range(1, 10);
    }

    void Start()
    {
        Debug.Log("Amount of resources: (1) " + _resourceAmount);
    }
}
