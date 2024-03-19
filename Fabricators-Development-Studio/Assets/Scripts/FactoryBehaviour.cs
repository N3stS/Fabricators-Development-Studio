using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FactoryBehaviour : MonoBehaviour
{
    private GameObject _factoryType;

    private bool idle;
    private bool resourceAmountMet;
    private bool productsReady;

    void Awake()
    {
        idle = true;
        resourceAmountMet = false;
        productsReady = false;
    }

    void Update()
    {
        
    }
}
