using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerTypeChest : MonoBehaviour
{
    [HideInInspector] public int _storageAmount;
    
    public void Awake()
    {
        _storageAmount = 10;
    }
}
