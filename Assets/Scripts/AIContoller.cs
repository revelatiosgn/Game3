using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace DSRPG
{
    public class AIContoller : MonoBehaviour
    {
        public Transform[] waypoints;
        public int currentWaypoint = 0;

        NavMeshAgent navMeshAgent;

        void Awake()
        {
            navMeshAgent = GetComponent<NavMeshAgent>();
        }

        void Start()
        {
            navMeshAgent.SetDestination(waypoints[currentWaypoint].position);
        }

        void Update()
        {
            Move();
        }

        void Move()
        {
            if (waypoints.Length == 0)
                return;
            
            Transform waypoint = waypoints[currentWaypoint];
            if (Vector3.Distance(transform.position, waypoint.position) <= 2f)
            {
                currentWaypoint = (currentWaypoint + 1) % waypoints.Length;
                navMeshAgent.SetDestination(waypoints[currentWaypoint].position);
            }
        }
    }
}
