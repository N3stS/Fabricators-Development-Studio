using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PeasantBehaviour : MonoBehaviour
{
    private PeasantState _peasantState;
    private PeasantMovement _peasantMovement;

    private List<GameObject> _work = new List<GameObject>();
    private List<GameObject> _chests = new List<GameObject>();
    private List<GameObject> _factory = new List<GameObject>();
    
    private GameObject _target;
    private Transform _targetPos;
    private Transform[] _newTarget;
    private List<GameObject> workObjectives = new List<GameObject>();
    private float dist;
    private float Dist;
    private GameObject tempObjective;
    private GameObject TempObjective;
    int count;

    void Awake()
    {
        _peasantState = gameObject.GetComponent<PeasantState>();
        _peasantMovement = gameObject.GetComponent<PeasantMovement>();
        
        foreach(GameObject workObj in GameObject.FindGameObjectsWithTag("Work"))
        {
            _work.Add(workObj);
        }
        foreach (GameObject workObj in GameObject.FindGameObjectsWithTag("Chests"))
        {
            _chests.Add(workObj);
        }
        foreach (GameObject workObj in GameObject.FindGameObjectsWithTag("Factory"))
        {
            _factory.Add(workObj);
        }
        
        if (_peasantState._idle)
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
        if (_peasantState._moving)
        {
            var step = GetComponent<PeasantMovement>()._peasantSpeed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, _targetPos.position, step);
        }
        else if (_peasantState._stopped)
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
        if (_peasantState._idle)
        {
            if (_peasantState._emptyHanded)
            {
                LookingForWork();
            }
            else if (_peasantState._carrying)
            {
                LookingForChest();
            }
            else
            {
                _peasantState.Error();
            }
        }
        else if (_peasantState._busy)
        {
            if (_peasantState._carrying)
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
        
        foreach (GameObject workObj in _chests)
        {
            Debug.Log("Chest Loop Started");
            TempObjective = workObj;

            Dist = Vector3.Distance(TempObjective.transform.position, transform.position);
            if (Dist < dist)
            {
                dist = Dist;
                tempObjective = TempObjective;
                Debug.Log(tempObjective + " " + dist);
            }
            count++;
            Debug.Log("Chest Loop: " + count);
        }
        
        _target = tempObjective;
        _targetPos = _target.transform;
        Debug.Log("Found Work: " + _target.name);
        
        _peasantState.Moving();
        _peasantState.Working();
    }

    void LookingForWork()
    {
        float dist = 999999999;
        float Dist = 999999999;

        GameObject tempObjective = null;
        GameObject TempObjective = null;
        
        foreach (GameObject workObj in _work)
        {
            Debug.Log("Work Loop Started");
            TempObjective = workObj;

            Dist = Vector3.Distance(TempObjective.transform.position, transform.position);
            if (Dist < dist)
            {
                dist = Dist;
                tempObjective = TempObjective;
                Debug.Log(tempObjective + " " + dist);
            }
            count++;
            Debug.Log("Work Loop: " + count);
        }
        
        _target = tempObjective;
        _targetPos = _target.transform;
        Debug.Log("Found Work: " + _target.name);
        
        _peasantState.Moving();
        _peasantState.Working();
    }

    void TargetReached()
    {
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
                _peasantState.EmptyHanded();
                
                TargetReached();
            }
            else if (col.CompareTag("Work"))
            {
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
    
    
    
    
    
    
    /*void NoCurrentJob()
    {
        _peasantJobFound = true;

        JobFound();
    }

    void JobFound()
    {
        if (_peasantObjectiveFound)
        {
            WorkReady();
        }
        else
        {
            NoCurrentObjective();
        }
    }

    void NoCurrentObjective()
    {
        if (!_peasantObjectiveFound)
        {
            Dist = 9999999;
            dist = 9999999;
            tempObjective = null;
            TempObjective = null;
            foreach (var workObj in workObjectives)
            {
                Debug.Log("Loop Started");
                TempObjective = workObj;

                Dist = Vector3.Distance(TempObjective.transform.position, transform.position);
                if (Dist < dist)
                {
                    dist = Dist;
                    tempObjective = TempObjective;
                    Debug.Log(tempObjective + " " + dist);
                }
                /*else
                {
                    dist = Dist;
                    tempObjective = TempObjective;
                }
                count++;
                Debug.Log("Loop: " + count);
            }

            _peasantObjective = tempObjective;
            //_peasantObjective = GameObject.FindGameObjectWithTag("Work");
            _targetPos = _peasantObjective.transform;
            Debug.Log("Found Work: " + _peasantObjective.name);
            _peasantObjectiveFound = true;
        }
        else if (_peasantObjectiveFound && _peasantObjective.tag == "Work")
        {
            _peasantObjective = null;
            _peasantObjective = GameObject.FindGameObjectWithTag("Chests");
            _targetPos = _peasantObjective.transform;
            Debug.Log("Found Work: " + _peasantObjective.name);
            _peasantObjectiveFound = true;
        }
        else if (_peasantObjectiveFound && _peasantObjective.tag == "Chests")
        {
            //_peasantObjective = null;
            //_peasantObjective = GameObject.FindGameObjectWithTag("Work");
            //_targetPos = _peasantObjective.transform;
            //Debug.Log("Found Work: " + _peasantObjective.name);
            //_peasantObjectiveF
        }

        WorkReady();
    }

    void WorkReady()
    {
        if (_peasantJobFound == true && _peasantObjectiveFound == true)
        {
            _peasantWorkReady = true;
            Debug.Log("Work Ready!");
        }
        else
        {
            Awake();
        }
    }

    void NotMovingTowardsObjective()
    {

    }

    void MovingTowardsObjective()
    {

    }

    void NotPerformingJob()
    {

    }

    void PerformingJob()
    {

    }

    Transform GetClosestChest (Transform[] chests)
    {
        Transform bestTarget = null;
        float closestDistanceSqr = Mathf.Infinity;
        Vector3 currentPosition = transform.position;
        foreach(Transform potentialTarget in chests)
        {
            Vector3 directionToTarget = potentialTarget.position - currentPosition;
            float dSqrToTarget = directionToTarget.sqrMagnitude;
            if(dSqrToTarget < closestDistanceSqr)
            {
                closestDistanceSqr = dSqrToTarget;
                bestTarget = potentialTarget;
            }
        }
     
        return bestTarget;
    }*/
}