using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceZoneMaterial : MonoBehaviour
{
    [SerializeField] private GameObject _zoneType;
    
    private int _resourceZoneAmount;

    void Awake()
    {
        _resourceZoneAmount = _zoneType.gameObject.GetComponent<ResourceTypeIron>()._resourceAmount;
    }
    
    void Start()
    {
        Debug.Log("Amount of resources: (2) " + _resourceZoneAmount);
    }

    public void ResourceRemoved()
    {
        _resourceZoneAmount--;
    }
}
