using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerBehaviour : MonoBehaviour
{
    [SerializeField] private GameObject _containerType;
    
    private List<string> _storageSpace = new List<string>();

    public int _targeted = 0;

    void Awake()
    {
        //_storageSpace.Capacity = _containerType.GetComponent<ContainerTypeFactory>()._storageAmount;
        //_storageSpace.Capacity = _containerType.GetComponent<ContainerTypeChest>()._storageAmount;
        if (gameObject.tag == "Chests")
        {
            _storageSpace.Capacity = 10;
        }
        if (gameObject.tag == "Factory")
        {
            _storageSpace.Capacity = 5;
        }
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

    public void ResourceAdded(string _item)
    {
        _storageSpace.Add(_item);

        Debug.Log("Item added: " + _item);
    }

    public void ResourceRemoved()
    {

    }
}
