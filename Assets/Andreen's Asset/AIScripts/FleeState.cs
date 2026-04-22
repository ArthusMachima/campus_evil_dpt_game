using UnityEngine;
using UnityEngine.AI;

public class FleeState : AIState
{
    private float fleeDistance = 10f;

    public FleeState(NavMeshAgent _agent, Transform _player, EnemyController _ctrl)
        : base(_agent, _player, _ctrl) { }

    public override void OnEnter()
    {
        agent.speed = 6f; // Run faster when scared
        RunAway();
    }

    public override void OnUpdate()
    {
        // If we reached our flee point and are far enough, go back to Patrol
        if (!agent.pathPending && agent.remainingDistance < 0.5f)
        {
            if (Vector3.Distance(agent.transform.position, player.position) > stateMachine.detectionRange)
            {
                stateMachine.ChangeState(new PatrolState(agent, player, stateMachine));
            }
            else
            {
                RunAway(); // Still too close? Keep running!
            }
        }
    }

    private void RunAway()
    {
        // 1. Calculate direction away from player
        Vector3 directionToPlayer = agent.transform.position - player.position;

        // 2. Define a target position in that direction
        Vector3 targetDestination = agent.transform.position + directionToPlayer.normalized * fleeDistance;

        // 3. CRITICAL: Find the nearest valid point on the NavMesh
        // This prevents the AI from sticking to the wall/edge
        if (NavMesh.SamplePosition(targetDestination, out NavMeshHit hit, fleeDistance, NavMesh.AllAreas))
        {
            agent.SetDestination(hit.position);
        }
    }
}