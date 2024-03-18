using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerBehaviour : MonoBehaviour
{
    [SerializeField] private GameObject _containerType;
    
    private List<string> _storageSpace = new List<string>();

    void Awake()
    {
        _storageSpace.Capacity = _containerType.GetComponent<ContainerTypeFactory>()._storageAmount;
    }

    public void ResourceAdded(string _item)
    {
        _storageSpace.Add(_item);

        Debug.Log("Item added: " + _item);
    }

    public void ResourceRemoved()
    {

    }
}
