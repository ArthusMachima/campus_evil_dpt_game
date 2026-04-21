using UnityEngine;
using UnityEngine.AI;
using UnityEngine.XR;

public class PatrolState : AIState
{
    private Vector3 _destination;
    public PatrolState(NavMeshAgent _agent, Transform _player, EnemyController _ctrl) :
        base(_agent, _player, _ctrl)
    { }

    public override void OnEnter()
    {
        SetNewRandomDestination();
    }

    public override void OnUpdate()
    {
        //Condition 1: Transition to Chase if player is within 10 meters
        float distanceToPlayer = Vector3.Distance(agent.transform.position, player.position);
        if (distanceToPlayer < stateMachine.detectionRange)
        {
            stateMachine.ChangeState(new ChaseState(agent, player, stateMachine));
            return;
        }

        //Condition 2: Pick a new spot if we arrived at the current one
        if(!agent.pathPending && agent.remainingDistance < 0.5f)
        {
            SetNewRandomDestination();
        }
    }

    private void SetNewRandomDestination()
    {
        Vector3 randomDir = Random.insideUnitSphere * 15f;
        randomDir += agent.transform.position;

        if (NavMesh.SamplePosition(randomDir, out NavMeshHit hit, 15f, 1))
        {
            agent.SetDestination(hit.position);
        }
    }
}
