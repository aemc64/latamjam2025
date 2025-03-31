using UnityEngine;

public class FoodStore : MonoBehaviour, IInteractable
{
    [SerializeField] private QuickTimeEvent _quickTimeEvent;
    
    public void OnInteract()
    {
        GameManager.Instance.RegisterInteractable(this);
        _quickTimeEvent.Initialize();
    }
}
