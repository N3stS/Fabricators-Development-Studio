using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerTypeFactory : MonoBehaviour
{
    [HideInInspector] public int _storageAmount;
    
    public void Awake()
    {
        _storageAmount = 5;
    }
}
