using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class AttackState : AIState
{
    private float attackCooldown = 1.5f; // seconds between attacks
    private float lastAttackTime;
    
    

    public AttackState(NavMeshAgent _agent, Transform _player, EnemyController _stateMachine)
        : base(_agent, _player, _stateMachine) { }

    public override void OnEnter()
    {
        agent.isStopped = true; // stop moving while attacking
        lastAttackTime = -attackCooldown; // allow immediate attack
    }


    public override void OnUpdate()
    {
        float distance = Vector3.Distance(agent.transform.position, player.position);

        // If player moves out of attack range, go back to chase
        if (distance > stateMachine.attackRange)
        {
            stateMachine.ChangeState(new ChaseState(agent, player, stateMachine));
            return;
        }

        // Face the player
        Vector3 direction = (player.position - agent.transform.position).normalized;
        agent.transform.rotation = Quaternion.LookRotation(direction);

        // Attack if cooldown elapsed
        if (Time.time >= lastAttackTime + attackCooldown)
        {
            PerformAttack();
            lastAttackTime = Time.time;
            Debug.LogWarning("is Attacking");
        }
    }

    public override void OnExit()
    {
        agent.isStopped = false; // resume movement when leaving attack
    }

    private void PerformAttack()
    {
        // Example: reduce player health
        /*PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
        if (playerHealth != null)
        {
            playerHealth.TakeDamage(stateMachine.attackDamage);
        }*/

        // You could also trigger an animation here
        // stateMachine.animator.SetTrigger("Attack");
        PlayerMotor playerH = player.GetComponent<PlayerMotor>();
        playerH.PlayerHealthBar(20);

    }
}
