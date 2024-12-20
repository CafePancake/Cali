using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "Events", menuName = "Create EventBundle")]
public class SOEvents : ScriptableObject
{
    //NORMAL EVENTS
    public UnityEvent disableAllInputs;
    public UnityEvent enableAllInputs;
    public UnityEvent intro;
    public UnityEvent interupt;
    public UnityEvent updateUI;

    //NUMBER EVENTS
    public UnityEvent casino = new UnityEvent();
    public UnityEvent sixNine = new UnityEvent();
    public UnityEvent boobs = new UnityEvent();
    public UnityEvent weed = new UnityEvent();
    public UnityEvent birthday = new UnityEvent();
    public UnityEvent today = new UnityEvent();
    public UnityEvent infinity = new UnityEvent();
    public UnityEvent NaNi = new UnityEvent();
    public numberEvent[] numberEvents;

    public void CreateNumberEventArray()
    {
        numberEvents = new numberEvent[]{        
        new numberEvent{relatedEvent=casino, number=777},
        new numberEvent{relatedEvent=sixNine, number = 69},
        new numberEvent{relatedEvent=boobs, number = 80085},
        new numberEvent{relatedEvent=weed, number = 420},
        new numberEvent{relatedEvent=birthday, number = 220507},
        new numberEvent{relatedEvent=today, number = DateTime.Now.Year*10000+DateTime.Now.Month*100+DateTime.Now.Day},
        new numberEvent{relatedEvent=infinity, number = float.PositiveInfinity},
        new numberEvent{relatedEvent=NaNi, number = float.NaN},
        };
    }
}

public class numberEvent
{
    public UnityEvent relatedEvent;
    public float number;
}
