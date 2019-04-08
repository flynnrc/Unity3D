using RPG.Combat;
using RPG.Control;
using RPG.Core;
using RPG.Movement;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : MonoBehaviour
{
    [SerializeField] float chaseDistance = 5f;
    Fighter fighter;
    Health health;
    Mover thisMover;
    GameObject player;

    Vector3 guardPosition;
    float timeSinceLastSawPlayer = Mathf.Infinity;
    [SerializeField] float suspicionTime = 5f;
    [SerializeField] PatrolPath patrolPath;
    [SerializeField] float WaypointTolerance = .1f;
    int currentWaypointIndex = 0;
    [SerializeField] float waypointDwellTime = 3f;
    float timeSinceArrivedAtWaypoint = Mathf.Infinity;


    private void Start()
    {
        fighter = GetComponent<Fighter>();
        health = GetComponent<Health>();
        player = GameObject.FindWithTag("Player");
        guardPosition = transform.position;
        thisMover = GetComponent<Mover>();
    }

    private void Update()
    {
        if (health.isAlive != true) { return; }
        if (InAttackRangeOfPlayer() && fighter.CanAttack(player))
        {
            AttackBehavior();
        }
        else if (timeSinceLastSawPlayer <= suspicionTime)
        {
            SuspicionBehavior();
        }
        else
        {
            PatrolBehavior();
        }

        UpdateTimers();
    }

    private void UpdateTimers()
    {
        timeSinceLastSawPlayer += Time.deltaTime;
        timeSinceArrivedAtWaypoint += Time.deltaTime;
    }

    private void SuspicionBehavior()
    {
        GetComponent<ActionScheduler>().CancelCurrentAction();
    }

    private void PatrolBehavior()
    {
        Vector3 nextPos = guardPosition;
        if(patrolPath != null)
        {
            if (AtWaypoint())
            {
                timeSinceArrivedAtWaypoint = 0;
                CycleWaypoint();
            }
            nextPos = GetCurrentWaypoint();
        }
        if(IsDwellTimeReached())
        {
            thisMover.StartMoveAction(nextPos);//consider look at as well
        }
        
    }

    private bool IsDwellTimeReached()
    {
        return timeSinceArrivedAtWaypoint > waypointDwellTime;
    }

    private Vector3 GetCurrentWaypoint()
    {
        return patrolPath.GetWaypoint(currentWaypointIndex);
    }

    private void CycleWaypoint()
    {
        currentWaypointIndex = patrolPath.GetNextIndex(currentWaypointIndex);
    }

    private bool AtWaypoint()
    {
        var test = Vector3.Distance(transform.position, GetCurrentWaypoint());
        return Vector3.Distance(transform.position, GetCurrentWaypoint()) < WaypointTolerance;
    }

    private void AttackBehavior()
    {
        timeSinceLastSawPlayer = 0;
        fighter.Attack(player);
    }

    private float DistanceToPlayer()
    {
        return Vector3.Distance(player.transform.position, transform.position);
    }

    private bool InAttackRangeOfPlayer()
    {
        return Vector3.Distance(player.transform.position, transform.position) < chaseDistance;
    }

    //called by unity
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, chaseDistance);
    }
}