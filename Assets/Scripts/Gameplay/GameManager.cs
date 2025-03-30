using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject _player;
    [SerializeField] private List<PoliceAI> _polices;
    
    public static GameManager Instance { get; private set; }
    
    public GameObject Player => _player;

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
}
