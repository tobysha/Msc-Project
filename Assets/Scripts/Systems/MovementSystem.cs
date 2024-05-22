using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MovementSystem : MonoBehaviour
{
    public Tilemap tilemap; // Tilemap reference
    public List<Vector3Int> waypoints; // Array of grid positions
    public float speed = 2f; // Movement speed

    private bool movingForward = true; // Direction flag
    private int currentWaypointIndex = 0; // Current waypoint index
    void Start()
    {
        //velocity = new VelocityComp(1, 0);
        //transform.position = tilemap.CellToWorld(waypoints[currentWaypointIndex]) + new Vector3(0.5f, 0.5f, 0);
    }
    public void InitializePath(Tilemap map, List<Vector3Int> path)
    {
        tilemap = map;
        waypoints = path;
        if (waypoints.Count > 0)
        {
            transform.position = tilemap.GetCellCenterWorld(waypoints[0]);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (waypoints == null || waypoints.Count == 0)
            return;

        Vector3 targetPosition = tilemap.GetCellCenterWorld(waypoints[currentWaypointIndex]);
        Vector3 direction = (targetPosition - transform.position).normalized;

        transform.position += direction * speed * Time.deltaTime;

        if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
            if (movingForward)
            {
                currentWaypointIndex++;
                if (currentWaypointIndex >= waypoints.Count)
                {
                    currentWaypointIndex = waypoints.Count - 1;
                    movingForward = false;
                }
            }
            else
            {
                currentWaypointIndex--;
                if (currentWaypointIndex < 0)
                {
                    currentWaypointIndex = 0;
                    movingForward = true;
                }
            }
        }
        //Vector3 vector3 = transform.position;
        //this.gameObject.transform.position = new Vector3(vector3.x + speed * velocity.GetVelocity_X() * Time.deltaTime, vector3.y + speed * velocity.GetVelocity_Y() * Time.deltaTime, 0f);

    }
}
