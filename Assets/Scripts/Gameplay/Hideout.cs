using UnityEngine;

public class Hideout : MonoBehaviour, IInteractable
{
    [SerializeField] private Transform _targetPosition;

    private bool _isPlayerHidden;
    
    public void OnInteract()
    {
        var player = GameManager.Instance.Player;
        
        if (!_isPlayerHidden)
        {
            GameManager.Instance.RegisterInteractable(this);
            player.EnableSprite(false);
            _isPlayerHidden = true;
        }
        else
        {
            GameManager.Instance.UnregisterInteractable();
            player.transform.position = _targetPosition.position;
            player.EnableSprite(true);
            _isPlayerHidden = false;
        }
    }
}
