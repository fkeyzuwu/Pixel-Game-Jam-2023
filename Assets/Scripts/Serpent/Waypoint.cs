using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    public bool isStoppingPoint;
    [Range(0.0f, 10.0f)] public float stopTime;
}
