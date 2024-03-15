using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PeasantBehaviour : MonoBehaviour
{
    [SerializeField] private GameObject _peasantJob;
    [SerializeField] private GameObject _peasantObjective;
    private bool _peasantWorkReady = true;
    private bool _peasantJobFound;
    private bool _peasantObjectiveFound;
    private Transform _target;
    private Transform[] _newTarget;

    void Awake()
    {
        _target = _peasantObjective.transform;
        
        _peasantWorkReady = true;

        if (_peasantJob == null)
        {
            NoCurrentJob();
        }
        else if (_peasantJob != null)
        {
            JobFound();
        }

        if (_peasantObjective == null)
        {
            NoCurrentObjective();
        }
        else if (_peasantObjective != null)
        {
            ObjectiveFound();
        }
    }

    void Update()
    {
        Debug.Log("Indeed");
        if (_peasantWorkReady)
        {
            var step = GetComponent<PeasantMovement>()._peasantSpeed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, _target.position, step);
            Debug.Log("Speed is: " + transform.position);
        }
    }
    void NoCurrentJob()
    {

    }

    void JobFound()
    {
        _peasantJobFound = true;
        if (_peasantJobFound == true && _peasantObjectiveFound == true) 
        {
            _peasantWorkReady = true;
        }
    }

    void NoCurrentObjective()
    {

    }

    void ObjectiveFound()
    {
        _peasantObjectiveFound = true;

        if (_peasantJobFound == true && _peasantObjectiveFound == true)
        {
            _peasantWorkReady = true;
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

    void OnTriggerEnter2D(Collider2D col)
    {
        _target = null;
        GetClosestChest(_target);
        //_target = _newTarget
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
    }
}