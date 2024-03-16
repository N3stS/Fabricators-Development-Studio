using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PeasantBehaviour : MonoBehaviour
{
    [SerializeField] private GameObject _peasantJob;
    private GameObject _peasantObjective;
    private bool _peasantWorkReady = false;
    private bool _peasantJobFound = true;
    private bool _peasantObjectiveFound = false;
    private Transform _target;
    private Transform[] _newTarget;

    void Awake()
    {
        if (_peasantJob == null)
        {
            NoCurrentJob();
        }
        else if (_peasantJob != null)
        {
            JobFound();
        }
    }

    void Update()
    {
        Debug.Log("Indeed");
        if (_peasantWorkReady)
        {
            var step = GetComponent<PeasantMovement>()._peasantSpeed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, _target.position, step);
        }
    }
    void NoCurrentJob()
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
            _peasantObjective = GameObject.FindGameObjectWithTag("Work");
            _target = _peasantObjective.transform;
            Debug.Log("Found Work: " + _peasantObjective.name);
            _peasantObjectiveFound = true;
        }
        else if (_peasantObjectiveFound && _peasantObjective.tag == "Work")
        {
            _peasantObjective = null;
            _peasantObjective = GameObject.FindGameObjectWithTag("Chests");
            _target = _peasantObjective.transform;
            Debug.Log("Found Work: " + _peasantObjective.name);
            _peasantObjectiveFound = true;
        }
        else if (_peasantObjectiveFound && _peasantObjective.tag == "Chests")
        {
            _peasantObjective = null;
            _peasantObjective = GameObject.FindGameObjectWithTag("Work");
            _target = _peasantObjective.transform;
            Debug.Log("Found Work: " + _peasantObjective.name);
            _peasantObjectiveFound = true;
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

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col = _peasantObjective.GetComponent<BoxCollider2D>())
        {
            _target = null;

            NoCurrentObjective();
        }
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