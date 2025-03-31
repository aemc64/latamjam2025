using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField] private float _speed = 10.0f;
    
    private InputAction _moveAction;
    private InputAction _interactAction;
    private Animator _animator;

    private bool _canMove = true;
    private IInteractable _currentInteractable;

    private void Start()
    {
        _moveAction = InputSystem.actions.FindAction("Move");
        _animator = GetComponent<Animator>();
        _interactAction = InputSystem.actions.FindAction("Jump");
    }

    private void Update()
    {
        if (_canMove)
        {
            var moveValue = _moveAction.ReadValue<Vector2>();
            transform.position += (Vector3)moveValue * (_speed * Time.deltaTime);
            _animator.SetBool("isMovingFront", true);
        }
        
        if (_currentInteractable != null && _interactAction.WasPerformedThisFrame())
        {
            _currentInteractable.OnInteract();
        }
    }

    public void EnableMovement(bool enable)
    {
        _canMove = enable;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (_currentInteractable == null && other.TryGetComponent(out IInteractable interactable))
        {
            _currentInteractable = interactable;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.TryGetComponent(out IInteractable interactable) && _currentInteractable == interactable)
        {
            _currentInteractable = null;
        }
    }
}
