using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class Agent : MonoBehaviour
{
    [SerializeField] public Transform playerTransform;
    [SerializeField] public float detectionRange = 10f;
    [SerializeField] public float patrolRadius = 20f;
    [SerializeField] private NavMeshAgent agent;
    private State currentState;
    private Vector3 patrolTarget;

    public enum State
    {
        Idle,
        Chasing,
        Patrolling
    }

    void Start()
    {
        currentState = State.Idle;
    }

    void Update()
    {
        switch (currentState)
        {
            case State.Idle:
                IdleUpdate();
                break;
            case State.Chasing:
                ChasingUpdate();
                break;
            case State.Patrolling:
                PatrollingUpdate();
                break;
        }

    }
    void IdleUpdate()
    {
        if (Vector3.Distance(transform.position, playerTransform.position) <= detectionRange)
        {
            currentState = State.Chasing;
        }
        else
        {
            currentState = State.Patrolling;
        }
    }

    void ChasingUpdate()
    {
        agent.SetDestination(playerTransform.position);

        if (Vector3.Distance(transform.position, playerTransform.position) > detectionRange)
        {
            currentState = State.Patrolling;
        }
    }

    void PatrollingUpdate()
    {
        if (!agent.pathPending && agent.remainingDistance < 0.5f)
        {
            SetRandomPatrolTarget();
        }

        if (Vector3.Distance(transform.position, playerTransform.position) <= detectionRange)
        {
            currentState = State.Chasing;
        }
    }

    void SetRandomPatrolTarget()
    {
        Vector3 randomDirection = Random.insideUnitSphere * patrolRadius;
        randomDirection += transform.position;
        NavMesh.SamplePosition(randomDirection, out NavMeshHit hit, patrolRadius, 1);
        patrolTarget = hit.position;
        agent.SetDestination(patrolTarget);
    }
}
