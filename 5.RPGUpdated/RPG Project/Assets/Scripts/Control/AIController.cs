using RPG.Combat;
using RPG.Core;
using RPG.Movement;
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

    Vector3 guardLocation;
    float timeSinceLastSawPlayer = Mathf.Infinity;
    [SerializeField] float suspicionTime = 5f;

    private void Start()
    {
        fighter = GetComponent<Fighter>();
        health = GetComponent<Health>();
        player = GameObject.FindWithTag("Player");
        guardLocation = transform.position;
        thisMover = GetComponent<Mover>();
    }

    private void Update()
    {
        if(health.isAlive != true) { return; }
        if (InAttackRangeOfPlayer() && fighter.CanAttack(player))
        {
            timeSinceLastSawPlayer = 0;
            AttackBehavior();
        }
        else if (timeSinceLastSawPlayer <= suspicionTime)
        {
            SuspicionBehavior();
        }
        else
        {
            GuardPositionBehavior();
        }

        timeSinceLastSawPlayer += Time.deltaTime;
    }

    private void SuspicionBehavior()
    {
        GetComponent<ActionScheduler>().CancelCurrentAction();
    }

    private void GuardPositionBehavior()
    {
        fighter.Cancel();
        thisMover.StartMoveAction(guardLocation);//consider look at as well
    }

    private void AttackBehavior()
    {
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