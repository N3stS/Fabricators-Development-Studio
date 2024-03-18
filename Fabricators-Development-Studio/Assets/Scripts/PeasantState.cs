using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeasantState : MonoBehaviour
{
    public bool _idle;
    public bool _busy;
    public bool _moving;
    public bool _stopped;
    public bool _error;
    public bool _emptyHanded;
    public bool _carrying;

    void Awake()
    {
        Idling();
        Stopped();
        EmptyHanded();
    }
    
    public void Idling()
    {
        _busy = false;
        _idle = true;
    }
    
    public void Working()
    {
        _idle = false;
        _busy = true;
    }
    
    public void Moving()
    {
        _stopped = false;
        _moving = true;
    }
    
    public void Stopped()
    {
        _moving = false;
        _stopped = true;
    }
    
    public void Error()
    {
        Debug.Log("Something Seems To Be Wrong :(");
    }

    public void EmptyHanded()
    {
        _carrying = false;
        _emptyHanded = true;
    }

    public void Carrying()
    {
        _emptyHanded = false;
        _carrying = true;
    }
}
