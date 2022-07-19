using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class StateController : MonoBehaviour
{
    public EnemyStats EnemyStats;
    public Transform EyesTransfrom;
    public State CurrentState;
    public State RemainState;

    [HideInInspector] public NavMeshAgent Agent;
    [HideInInspector] public List<Transform> Waypoints;
    [HideInInspector] public int NextWaypoint;
    [HideInInspector] public Transform Target;
    [HideInInspector] public Vector3 LastKnowTargetPostition;
    [HideInInspector] public bool StateBool;
    [HideInInspector] public float stateTimeElapsed;

    private bool isActive;

    private void Awake()
    {
        Agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        if (!isActive) return;
        CurrentState.UpdateState(this);
    }

    public void InitializeAI(bool activate, List<Transform> waypointList)
    {
        Waypoints = waypointList;
        isActive = activate;
        Agent.enabled = isActive;
    }

    public void TransitionToState(State nextState)
    {
        if (nextState != RemainState)
        {
            CurrentState = nextState;
            OnExitState();
        }
    }

    private void OnExitState()
    {
        StateBool = false;
        stateTimeElapsed = 0;
    }

    public bool HasTimeElapsed(float duration)
    {
        stateTimeElapsed += Time.deltaTime;
        if (stateTimeElapsed >= duration)
        {
            stateTimeElapsed = 0;
            return true;
        }
        else
            return false;
    }


    private void OnDrawGizmos()
    {
        if (CurrentState != null)
        {
            Gizmos.color = CurrentState.GizmoColor;
            Gizmos.DrawWireSphere(EyesTransfrom.position, 1.5f);
        }
    }
}
