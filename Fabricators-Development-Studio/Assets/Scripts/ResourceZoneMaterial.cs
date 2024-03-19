using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceZoneMaterial : MonoBehaviour
{
    [SerializeField] private GameObject _zoneType;

    public int _targeted = 0;

    private int _resourceZoneAmount;

    void Awake()
    {
        _resourceZoneAmount = _zoneType.gameObject.GetComponent<ResourceTypeIron>()._resourceAmount;
    }
    
    void Start()
    {
        Debug.Log("Amount of resources: (2) " + _resourceZoneAmount);
    }

    private void Update()
    {
        if (_targeted > 0)
        {
            gameObject.GetComponent<BoxCollider2D>().isTrigger = true;
        }
        else if (_targeted <= 0)
        {
            gameObject.GetComponent<BoxCollider2D>().isTrigger = false;
        }
    }

    public void ResourceRemoved()
    {
        _resourceZoneAmount--;
    }
}
