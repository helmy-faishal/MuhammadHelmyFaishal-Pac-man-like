using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public List<Transform> Waypoints;
    public NavMeshAgent Agent;

    public Player player;
    public float ChaseDistance;

    public bool IsNearPlayer
    {
        get
        {
            if (player == null || player.IsRespawn)
            {
                return false;
            }

            return Vector3.Distance(transform.position, player.transform.position) < ChaseDistance;
        }
    }

    BaseState _currentState;

    [HideInInspector]
    public PatrolState PatrolState = new PatrolState();

    [HideInInspector]
    public ChaseState ChaseState = new ChaseState();

    [HideInInspector]
    public RetreatState RetreatState = new RetreatState();

    [HideInInspector]
    public Animator animator;

    private void Awake()
    {
        Agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

        _currentState = PatrolState;
        _currentState.EnterState(this);
    }

    private void Start()
    {
        if (player != null)
        {
            player.OnPowerUpStart += StartRetreat;
            player.OnPowerUpStop += StopRetreat;
        }
    }

    private void OnDisable()
    {
        if (player != null)
        {
            player.OnPowerUpStart -= StartRetreat;
            player.OnPowerUpStop -= StopRetreat;
        }
    }

    void Update()
    {
        if (_currentState != null)
        {
            _currentState.UpdateState(this);
        }
    }

    public void SwitchState(BaseState state)
    {
        _currentState.ExitState(this);
        _currentState = state;
        _currentState.EnterState(this);
    }

    void StartRetreat()
    {
        SwitchState(RetreatState);
    }

    void StopRetreat()
    {
        SwitchState(PatrolState);
    }

    public void EnemyDead()
    {
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (_currentState != ChaseState) return;

        if (collision.collider.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<Player>().TakeDamage();
            SwitchState(PatrolState);
        }
    }
}
