using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class ChaseState : AIState
{
    //The construnctir must match the new 'EbemtController' type
    public ChaseState(NavMeshAgent _agent, Transform _player, EnemyController _stateMachine)
        : base(_agent, _player, _stateMachine) { }

    public override void OnEnter()
    {
        //Increase speed for chasing
        agent.speed = 3.0f;
        agent.stoppingDistance = 2.0f;
    }

    public override void OnUpdate()
    {
        // If the agent is stuck or lost its path due to collision
        if (!agent.hasPath || agent.velocity.sqrMagnitude < 0.1f)
        {
            agent.SetDestination(player.position);
        }

        //1. Movement Logic : Follow the player
        agent.SetDestination(player.position);

        //2. Condition: if player gets too far away, go back to patrolling
        float distance = Vector3.Distance(agent.transform.position, player.position);
        if (distance > stateMachine.detectionRange + 2f) // Added a small buffer so they don't stutter at the edge
        {
            stateMachine.ChangeState(new PatrolState(agent, player, stateMachine));
        }

        //3. Condition : if player is very close, you could trigger an Attact state
        if (distance < stateMachine.attackRange)
        {
            stateMachine.ChangeState(new AttackState(agent, player, stateMachine));
        }

        else
        {
            agent.SetDestination(player.position);
        }
    }

    public override void OnExit()
    {
        // Reset speed or stopping distance when leaving the state
        agent.stoppingDistance = 0.5f;
    }
}
