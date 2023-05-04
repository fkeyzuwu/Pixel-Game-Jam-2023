using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    //dont change these
    [SerializeField] private GameObject platform;
    [SerializeField] private Waypoint waypoint1;
    [SerializeField] private Waypoint waypoint2;

    private Waypoint currentWaypoint;

    [SerializeField] private float platformSpeed;

    private void Start()
    {
        currentWaypoint = waypoint2;
    }

    private void Update()
    {
        if (Vector2.Distance(platform.transform.position, currentWaypoint.transform.position) > 0.1f)
        {
            Vector2 direction = (currentWaypoint.transform.position - platform.transform.position).normalized;
            platform.transform.position += new Vector3(direction.x * platformSpeed, direction.y * platformSpeed) * Time.deltaTime;
        }
        else
        {
            currentWaypoint = currentWaypoint == waypoint1 ? waypoint2 : waypoint1; //infinitely loop between the two waypoints
        }
    }
}
