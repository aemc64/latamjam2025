using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _speed = 10.0f;
    
    private InputAction _moveAction;

    private void Start()
    {
        _moveAction = InputSystem.actions.FindAction("Move");
    }

    private void Update()
    {
        var moveValue = _moveAction.ReadValue<Vector2>();
        transform.position += (Vector3)moveValue * (_speed * Time.deltaTime);
    }
}
