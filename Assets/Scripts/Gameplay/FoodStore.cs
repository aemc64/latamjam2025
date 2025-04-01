using UnityEngine;

public class FoodStore : MonoBehaviour, IInteractable
{
    [SerializeField] private QuickTimeEvent _quickTimeEvent;
    
    private bool _quickTimeInProgress;
    
    public void OnInteract()
    {
        if (_quickTimeInProgress)
        {
            return;
        }
        
        GameManager.Instance.RegisterInteractable(this);
        _quickTimeEvent.Initialize(this);
    }

    public void OnQuickTimeEventFinished(bool success)
    {
        GameManager.Instance.UnregisterInteractable();
        _quickTimeInProgress = false;
    }
}
