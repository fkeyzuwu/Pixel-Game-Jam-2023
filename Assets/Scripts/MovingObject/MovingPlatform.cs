using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject platform;
    [SerializeField] private Material redOutline;
    [SerializeField] private Material purpleOutline;
    private Collider2D[] platformColliders;
    private SpriteRenderer platformRenderer;
    [SerializeField] private Waypoint waypoint1;
    [SerializeField] private Waypoint waypoint2;

    [Header("Properties")]
    [SerializeField] private Universe platformUniverse;
    [SerializeField] private float platformSpeed;

    private Waypoint currentWaypoint;


    private void Start()
    {
        currentWaypoint = waypoint2;
        platformRenderer = platform.GetComponent<SpriteRenderer>();

        if(platformUniverse != Universe.None)
        {
            platformRenderer.material = platformUniverse == Universe.Red ? redOutline : purpleOutline;
        }

        platformColliders = platform.GetComponents<Collider2D>();

        UniverseSwitchManager.Instance.OnUniverseChangedCallback += TogglePlatformVisibility;
        TogglePlatformVisibility(UniverseSwitchManager.Instance.currentUniverse);
    }

    private void OnDestroy()
    {
        UniverseSwitchManager.Instance.OnUniverseChangedCallback -= TogglePlatformVisibility;
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


    private void TogglePlatformVisibility(Universe universe)
    {
        if (platformUniverse == Universe.None) return; //always show moving platform

        if(platformUniverse != universe) //hide platform
        {
            TogglePlatformVisibility(false);
        }
        else //show platform
        {
            TogglePlatformVisibility(true);
        }
    }

    private void TogglePlatformVisibility(bool visible) 
    {
        platformRenderer.enabled = visible;
        foreach (Collider2D collider in platformColliders)
        {
            collider.enabled = visible;
        }
    }
}
