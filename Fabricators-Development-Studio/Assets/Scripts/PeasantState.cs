using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeasantState : MonoBehaviour
{
    public bool idle;
    public bool busy;
    public bool moving;
    public bool stopped;
    public bool error;
    public bool emptyHanded;
    public bool carrying;

    void Awake()
    {
        Idling();
        Stopped();
        EmptyHanded();
    }
    
    public void Idling()
    {
        busy = false;
        idle = true;
    }
    
    public void Working()
    {
        idle = false;
        busy = true;
    }
    
    public void Moving()
    {
        stopped = false;
        moving = true;
    }
    
    public void Stopped()
    {
        moving = false;
        stopped = true;
    }
    
    public void Error()
    {
        Debug.Log("Something Seems To Be Wrong :(");
    }

    public void EmptyHanded()
    {
        carrying = false;
        emptyHanded = true;
    }

    public void Carrying()
    {
        emptyHanded = false;
        carrying = true;
    }
}
