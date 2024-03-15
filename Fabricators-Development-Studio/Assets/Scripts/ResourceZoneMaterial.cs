using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceZoneMaterial : MonoBehaviour
{
    [SerializeField] private GameObject _zoneType;
    private string _resourceZoneType;
    private int _resourceZoneAmount;

    void Awake()
    {
        _resourceZoneType = _zoneType.gameObject.GetComponent<ResourceTypeIron>()._resourceType;
        _resourceZoneAmount = _zoneType.gameObject.GetComponent<ResourceTypeIron>()._resourceAmount;
    }
    
    void Start()
    {
        Debug.Log("Type of resource " + _resourceZoneType);
        Debug.Log("Amount of resources: (2) " + _resourceZoneAmount);
    }
}
