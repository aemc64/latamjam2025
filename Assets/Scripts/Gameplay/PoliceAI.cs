using System;
using System.Collections.Generic;
using UnityEngine;

public class PoliceAI : MonoBehaviour
{
    [SerializeField] private List<Transform> _wanderingWaypoints;
    
    private int _currentWaypointIndex;
    private AITargetSetter _aiTargetSetter;

    private void Awake()
    {
        _aiTargetSetter = GetComponent<AITargetSetter>();
        _aiTargetSetter.OnDestinationReached += OnDestinationReached;
    }

    private void OnDestinationReached()
    {
        switch (_currentState)
        {
            case State.Wandering:
            {
                if (_currentWaypointIndex < _wanderingWaypoints.Count - 1)
                {
                    _currentWaypointIndex++;
                }
                else
                {
                    _currentWaypointIndex = 0;
                }
                break;
            }
        }
    }

    private enum State
    {
        Wandering,
        Chasing
    }
    
    private State _currentState = State.Wandering;

    private void Update()
    {
        switch (_currentState)
        {
            case State.Wandering:
            {
                UpdateWandering();
                break;
            }
            case State.Chasing:
            {
                UpdateChasing();
                break;
            }
        }
    }

    private void UpdateWandering()
    {
        _aiTargetSetter.target = _wanderingWaypoints[_currentWaypointIndex].position;
    }

    private void UpdateChasing()
    {
        _aiTargetSetter.target = GameManager.Instance.Player.transform.position;
    }

    public void Chase()
    {
        _currentState = State.Chasing;
    }

    public void Wander()
    {
        _currentState = State.Wandering;
    }
}
