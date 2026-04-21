using JetBrains.Annotations;
using System;
using System.IO.Compression;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;


[RequireComponent(typeof(NavMeshAgent))]
public class EnemyController : MonoBehaviour
{
    [Header("Movement & Drift")]
    [Tooltip("How fast the AI reaches max speed and stops. 100+ = Instant/No Drift.")]
    public float movementSnappiness = 100f;
    public bool stopImmediately = true;

    [Header("Detection Settings")]
    public Transform player; // Reference to the target

    [Header("Movement & Rotation")]
    public float rotationSpeed = 10f; // Higher = faster turning

    public float detectionRange = 10f;
    public float attackRange = 2.0f;
    public float patrolRadius = 15f;

    [Header("Gizmo Colors")]
    public Color detectColor = Color.yellow;
    public Color attackColor = Color.red;
    public Color patrolColor = Color.blue;

    private NavMeshAgent _agent;
    private AIState _currentState;

    void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();

        // 1. To remove drift, we need high acceleration
        _agent.acceleration = movementSnappiness;

        // 2. AutoBraking causes the AI to slow down gradually near its goal. 
        // Turning it off makes the stop more "sudden".
        _agent.autoBraking = !stopImmediately;


        // 1. IMPORTANT: Disable auto-rotation so we can control it via code
        _agent.updateRotation = false;

        GetComponent<Rigidbody>().isKinematic = true;

        // 2. Set Rigidbody to Kinematic to prevent the "stuck at edge" bug
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.isKinematic = true;
    }

    void Start()
    {
        if (player == null) player = GameObject.FindWithTag("Player").transform;

        //Initialize with the first state (Patrol)
        ChangeState(new PatrolState(GetComponent<NavMeshAgent>(), player, this));
    }

   
    void Update()
    {

        // Update acceleration in real-time so you can tweak it in the Inspector
        _agent.acceleration = movementSnappiness;

        // Execute the logic of the current active state
        _currentState?.OnUpdate();
        ApplyCustomRotation();

    }

    private void ApplyCustomRotation()
    {
        // Only rotate if the agent is actually trying to move
        if (_agent.desiredVelocity.sqrMagnitude > 0.1f)
        {
            // Calculate the direction the agent wants to move
            Quaternion lookRotation = Quaternion.LookRotation(_agent.desiredVelocity.normalized);

            // Smoothly rotate the transform toward that direction
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);
        }
    }

    
    public void ChangeState(AIState newState)
    {
        _currentState?.OnExit();
        _currentState = newState;
        _currentState.OnEnter();

        //Debuggin tip: See what state the AI is in inside the Unity Console
        Debug.Log($"AI transitioned to: {newState.GetType().Name}");
    }

    //draw the ranfes of the values of attack, patrol and etc.
    private void OnDrawGizmos()
    {
        //1. Detection Range (Yellow)
        Gizmos.color = detectColor;
        Gizmos.DrawWireSphere(transform.position, detectionRange);

        //2. Attack Range (Red)
        Gizmos.color = attackColor;
        Gizmos.DrawWireSphere(transform.position, attackRange);

        //3. Patrol Area (blue) - Optional, show where theyt wander
        Gizmos.color = patrolColor;
        Gizmos.DrawWireSphere(transform.position, patrolRadius);
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Check if we are still on the NavMesh after the hit
            NavMeshHit hit;
            if (NavMesh.SamplePosition(transform.position, out hit, 1.0f, NavMesh.AllAreas))
            {
                // Reset the agent to the nearest valid point to prevent "Edge Sticking"
                GetComponent<NavMeshAgent>().Warp(hit.position);
            }
        }
    }
}
