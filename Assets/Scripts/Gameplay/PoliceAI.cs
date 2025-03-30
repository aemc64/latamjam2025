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
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private enum State
    {
        Wandering
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
        }
    }

    private void UpdateWandering()
    {
        _aiTargetSetter.target = _wanderingWaypoints[_currentWaypointIndex].position;
    }
}
