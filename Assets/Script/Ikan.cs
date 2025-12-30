using UnityEngine;
using System.Linq;
using System.Collections.Generic;
using Random;
using Unity.VisualScripting;

public class Ikan : MonoBehaviour
{
    private bool onTake;
    private bool isWayPointSet;
    [SerializeField] private AIWaypoint currWaypoint;
    [SerializeField] private AIWaypoint prevWaypoint;
    private AIWaypointsGroup waypointsGroup;
    private Rigidbody rb;

    public float speed;
    public float cooldown;
    public float interval;

    void Start()
    {
        cooldown = Time.time + interval;

        rb = GetComponent<Rigidbody>();

        //var closestWayPointGroup = FindClosestWaypointsGroup();
        //waypointsGroup = startPointGroup;

    }
    void Update()
    {
        if(!onTake && cooldown <= Time.time)
        {
            if (!OnPosition())
            {
                rb.linearVelocity = Direction().normalized * speed;   
            }
            else
            {
                isWayPointSet = false;
                rb.linearVelocity = Vector3.zero;
                cooldown = Time.time + interval;
            }
        }
    }

    public void Take()
    {
        onTake = true;
        rb.linearVelocity = Vector2.zero;

    }

    public void Drop()
    {
        //onTake = false;
        //var closestWayPointGroup = FindClosestWaypointsGroup();
        //waypointsGroup = endPointGroup;
        GetComponent<BoxCollider>().enabled = false;
    }

    

    public Pair<AIWaypointsGroup, AIWaypoint> FindClosestWaypointsGroup()
    {
        AIWaypointsGroup[] allGroups = FindObjectsByType<AIWaypointsGroup>(FindObjectsSortMode.InstanceID);
        AIWaypointsGroup closestGroup = null;
        AIWaypoint closestWaypoint = null;
        float distance = Mathf.Infinity;
        foreach (var group in allGroups)
        {
            foreach (var waypoint in group.Waypoints)
            {
                if(waypoint == null) 
                    continue;

                Vector3 pointPos = waypoint.transform.position;
                float waypointDistance = DistanceOf(pointPos);

                if(waypointDistance < distance)
                {
                    closestGroup = group;
                    closestWaypoint = waypoint;
                }
            }
        }

        return new(closestGroup, closestWaypoint);
    }

    public AIWaypoint[] GetFreeWaypoints(AIWaypointsGroup group)
    {
        if (group == null || group.Waypoints.Count == 0)
            return null;

        return group.Waypoints.Where(x => x.ReservedBy == null).ToArray();
    }

    public float DistanceOf(Vector3 target)
    {
        return Vector3.Distance(transform.position, target);
    }

    public void SetNextWaypoint()
    {
        prevWaypoint = currWaypoint;
        if (prevWaypoint != null)
            prevWaypoint.ReservedBy = null;

        var freeWaypoints = GetFreeWaypoints(waypointsGroup);

        freeWaypoints = freeWaypoints.Except(new[] { prevWaypoint }).ToArray();
        currWaypoint = freeWaypoints.Random();
        transform.LookAt(currWaypoint.transform);
    }


    private bool OnPosition()
    {
        if(!waypointsGroup) return true;

        if(isWayPointSet){
            return DistanceOf(currWaypoint.transform.position) <= .5f;
        }
        else
        {
            
            SetNextWaypoint();
            isWayPointSet = true;
            return false;
        }
    }
    

    private Vector3 Direction()
    {
        if(!currWaypoint || OnPosition()) return Vector3.zero;

        return currWaypoint.transform.position - transform.position;
    }

    public struct Pair<T1, T2>
    {
        public T1 Key { get; set; }
        public T2 Value { get; set; }
        public bool IsAssigned
        {
            get => Key != null && Value != null;
        }

        public Pair(T1 key, T2 value)
        {
            Key = key;
            Value = value;
        }
    }
}

