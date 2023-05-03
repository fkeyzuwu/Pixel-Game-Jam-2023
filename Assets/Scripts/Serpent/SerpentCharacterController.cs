using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SerpentCharacterController : CharacterBasicController
{
    [SerializeField] private Transform waypointObject;
    private List<Waypoint> waypoints = new List<Waypoint>();
    private int currentWaypointIndex = 0;
    private bool finishedMoving = false;

    private float floatPhase = 0f;
    private float floatSpeed = 0.1f;
    private float floatAmount = 35f; //the higher the value, the less extreme the effect

    private bool isStopped = false;
    private void Awake()
    {
        foreach(Transform child in waypointObject) 
        {
            waypoints.Add(child.GetComponent<Waypoint>());
        }
    }

    private void FixedUpdate()
    {
        Float();

        if (finishedMoving || isStopped) return;

        if (Vector2.Distance(transform.position, waypoints[currentWaypointIndex].transform.position) > 0.1f)
        {
            Vector2 direction = waypoints[currentWaypointIndex].transform.position - transform.position;
            Move(direction.normalized);
        }
        else
        {
            if (waypoints.Count > currentWaypointIndex + 1)
            {
                currentWaypointIndex++;
                if (waypoints[currentWaypointIndex].isStoppingPoint) 
                {
                    StartCoroutine(StopAtWaypoint(waypoints[currentWaypointIndex].stopTime));
                }
            }
            else
            {
                finishedMoving = true;
                IsWalking = false;
                MoveDelta = Vector2.zero;
                Rigidbody.velocity = Vector2.zero;
            }
        }
    }

    protected override void Move(Vector2 moveDelta)
    {
        IsWalking = moveDelta != Vector2.zero;
        MoveDelta = moveDelta;
        Rigidbody.velocity = new Vector2(moveDelta.x * movementSpeed, moveDelta.y * movementSpeed);
    }

    private void Float() 
    {
        floatPhase = (floatPhase + floatSpeed) % 360; //infinite cycle
        var currentPos = transform.position;
        transform.position = new Vector2(currentPos.x, currentPos.y + Mathf.Sin(floatPhase) / floatAmount);
    }

    IEnumerator StopAtWaypoint(float waypointStopTime) 
    {
        isStopped = true;
        IsWalking = false;
        MoveDelta = Vector2.zero;
        Rigidbody.velocity = Vector2.zero;
        yield return new WaitForSeconds(waypointStopTime);
        isStopped = false;
    }
}
