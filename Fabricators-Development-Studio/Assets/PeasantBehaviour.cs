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
        foreach(GameObject workObj in GameObject.FindGameObjectsWithTag("Work"))
        {
            workObjectives.Add(workObj);
        }
        if (_peasantJob == null)
        {
            NoCurrentJob();
        }
        else if (_peasantJob != null)
        {
            JobFound();
        }
    }

    void Start()
    {
        
    }

    void Update()
    {
        Debug.Log("Indeed");
        if (_peasantWorkReady)
        {
            var step = GetComponent<PeasantMovement>()._peasantSpeed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, _targetPos.position, step);
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
                else
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
            _peasantObjective = null;
            _peasantObjective = GameObject.FindGameObjectWithTag("Work");
            _targetPos = _peasantObjective.transform;
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
            _targetPos = null;

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