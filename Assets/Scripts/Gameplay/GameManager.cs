using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private List<PoliceAI> _polices;
    
    public static GameManager Instance { get; private set; }

    private IInteractable _currentInteractable;
    
    public Player Player => _player;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            ChasePlayer();
        }
    }

    private void ChasePlayer()
    {
        foreach (var police in _polices)
        {
            police.Chase();
        }
    }

    public void RegisterInteractable(IInteractable interactable)
    {
        if (_currentInteractable != null)
        {
            return;
        }
        
        _currentInteractable = interactable;
        Player.EnableMovement(false);
    }

    public void UnregisterInteractable()
    {
        _currentInteractable = null;
        Player.EnableMovement(true);
    }
}
