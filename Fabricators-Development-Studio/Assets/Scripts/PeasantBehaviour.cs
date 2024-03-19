using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeasantBehaviour : MonoBehaviour
{
    private PeasantState _peasantState;
    private PeasantMovement _peasantMovement;

    private List<GameObject> Work = new List<GameObject>();
    private List<GameObject> Chests = new List<GameObject>();
    private List<GameObject> Factory = new List<GameObject>();
    
    private GameObject _target;
    
    private Transform _targetPos;
    private Transform[] _newTarget;

    private int count;
    [HideInInspector] public string _carriedItem;

    void Awake()
    {
        _peasantState = gameObject.GetComponent<PeasantState>();
        _peasantMovement = gameObject.GetComponent<PeasantMovement>();
        
        foreach(GameObject workObj in GameObject.FindGameObjectsWithTag("Work"))
        {
            Work.Add(workObj);
        }
        foreach (GameObject workObj in GameObject.FindGameObjectsWithTag("Chests"))
        {
            Chests.Add(workObj);
        }
        foreach (GameObject workObj in GameObject.FindGameObjectsWithTag("Factory"))
        {
            Factory.Add(workObj);
        }
        
        if (_peasantState.idle)
        {
            PeasantCheckState();
        }
        else
        {
            _peasantState.Error();
        }
    }

    void Update()
    {
        if (_peasantState.moving)
        {
            var step = GetComponent<PeasantMovement>()._peasantSpeed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, _targetPos.position, step);
        }
        else if (_peasantState.stopped)
        {
            var step = 0;
            transform.position = Vector3.MoveTowards(transform.position, _targetPos.position, step);
        }
        else
        {
            _peasantState.Error();
        }
    }

    void PeasantCheckState()
    {
        if (_peasantState.idle)
        {
            if (_peasantState.emptyHanded)
            {
                LookingForWork();
            }
            else if (_peasantState.carrying)
            {
                LookingForChest();
            }
            else
            {
                _peasantState.Error();
            }
        }
        else if (_peasantState.busy)
        {
            if (_peasantState.carrying)
            {
                LookingForChest();
            }
            else
            {
                _peasantState.Error();
            }
        }
    }

    void LookingForChest()
    {
        float dist = 999999999;
        float Dist = 999999999;

        GameObject tempObjective = null;
        GameObject TempObjective = null;
        
        foreach (GameObject workObj in Chests)
        {
            //Debug.Log("Chest Loop Started");
            TempObjective = workObj;

            Dist = Vector3.Distance(TempObjective.transform.position, transform.position);
            if (Dist < dist)
            {
                dist = Dist;
                tempObjective = TempObjective;
                //Debug.Log(tempObjective + " " + dist);
            }
            count++;
            //Debug.Log("Chest Loop: " + count);
        }
        
        _target = tempObjective;
        _target.GetComponent<ContainerBehaviour>()._targeted++;
        _targetPos = _target.transform;
        //Debug.Log("Found Chest: " + _target.name);
        
        _peasantState.Moving();
        _peasantState.Working();
    }

    void LookingForWork()
    {
        float dist = 999999999;
        float Dist = 999999999;

        GameObject tempObjective = null;
        GameObject TempObjective = null;
        
        foreach (GameObject workObj in Work)
        {
            //Debug.Log("Work Loop Started");
            TempObjective = workObj;

            Dist = Vector3.Distance(TempObjective.transform.position, transform.position);
            if (Dist < dist)
            {
                dist = Dist;
                tempObjective = TempObjective;
                //Debug.Log(tempObjective + " " + dist);
            }
            //count++;
            //Debug.Log("Work Loop: " + count);
        }
        
        _target = tempObjective;
        _targetPos = _target.transform;
        _target.GetComponent<ResourceZoneMaterial>()._targeted++;
        //Debug.Log("Found Work: " + _target.name);
        
        _peasantState.Moving();
        _peasantState.Working();
    }

    void TargetReached()
    {
        if (_target.tag == "Work")
        {
            _target.GetComponent<ResourceZoneMaterial>()._targeted--;
        }
        else if (_target.tag == "Chests")
        {
            _target.GetComponent<ContainerBehaviour>()._targeted--;
        }
      
        _target = null;
        
        _peasantState.Stopped();
        _peasantState.Idling();

        PeasantCheckState();
    }
    
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col = _target.GetComponent<BoxCollider2D>())
        {
            if (col.CompareTag("Chests"))
            {
                col.GetComponent<ContainerBehaviour>().ResourceAdded(_carriedItem);

                _carriedItem = null;

                _peasantState.EmptyHanded();
                
                TargetReached();
            }
            else if (col.CompareTag("Work"))
            {
                col.GetComponent<ResourceZoneMaterial>().ResourceRemoved();

                _carriedItem = "Iron";

                _peasantState.Carrying();
                
                TargetReached();
            }
            else if (col.CompareTag("Factory"))
            {
                _peasantState.EmptyHanded();
                
                TargetReached();
            }
            else
            {
                _peasantState.Error();
            }
        }
    }
}