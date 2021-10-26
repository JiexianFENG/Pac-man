using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tween
{
    public Transform Target { get; private set; }
    public Vector3 StartPos { get; private set; }
    public Vector3 EndPos { get; private set; }
    public float StartTime { get; private set; }
    public float Duration { get; private set; }
    public RectTransform recttarget { get; private set; }


    public Tween(Transform target, Vector3 origin, Vector3 destination, float startTime, float duration)
    {
        Target = target;
        StartPos = origin;
        EndPos = destination;
        StartTime = startTime;
        Duration = duration;
    }
    public Tween(RectTransform target, Vector3 origin, Vector3 destination, float startTime, float duration)
    {
        recttarget = target;
        StartPos = origin;
        EndPos = destination;
        StartTime = startTime;
        Duration = duration;
    }
}
