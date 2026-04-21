//using UnityEditorInternal;
using UnityEngine;
using UnityEngine.AI;

//this is the abstract blueprint for ANY AI behavior
public abstract class AIState
{
    protected NavMeshAgent agent;
    protected Transform player;
    protected EnemyController stateMachine;

    public AIState(NavMeshAgent _agent, Transform _player, EnemyController _stateMachine)
    {
        agent = _agent;
        player = _player;
        stateMachine = _stateMachine;
    }

    public virtual void OnEnter() { } //Runs once when starting the state
    public virtual void OnUpdate() { }//Runs every framce
    public virtual void OnExit() { } //Runs once when leaving the state
    
    
}
