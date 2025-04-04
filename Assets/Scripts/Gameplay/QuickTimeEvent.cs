using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class QuickTimeEvent : MonoBehaviour
{
    [SerializeField] private float _time = 5.0f;
    [SerializeField] private Image _barFill;
    [SerializeField] private InputImage _inputPrefab;
    [SerializeField] private int _numberOfInputs;
    [SerializeField] private RectTransform _inputsParent;
    
    private InputAction _moveDownAction;
    private InputAction _moveUpAction;
    private InputAction _moveLeftAction;
    private InputAction _moveRightAction;
    
    private List<InputImage> _inputImages = new List<InputImage>();
    private List<InputType> _inputTypes = new List<InputType>();
    
    private float _timer;
    private int _currentInput = 0;
    private FoodStore _foodStore;

    public void Initialize(FoodStore foodStore)
    {
        _foodStore = foodStore;
        
        _moveDownAction = InputSystem.actions.FindAction("MoveDown");
        _moveUpAction = InputSystem.actions.FindAction("MoveUp");
        _moveLeftAction = InputSystem.actions.FindAction("MoveLeft");
        _moveRightAction = InputSystem.actions.FindAction("MoveRight");
        
        for (var i = 0; i < _numberOfInputs; i++)
        {
            var inputImage = Instantiate(_inputPrefab, _inputsParent);
            var input = (InputType)Random.Range(0, Enum.GetValues(typeof(InputType)).Length - 1);
            inputImage.SetInputIcon(input);
            _inputImages.Add(inputImage);
            _inputTypes.Add(input);
        }

        _timer = _time;
        
        gameObject.SetActive(true);
    }

    private void Update()
    {
        if (_timer > 0)
        {
            _timer -= Time.deltaTime;
            
            _barFill.fillAmount = _timer / _time;
        }
        else
        {
            OnFail();
        }
        
        var currentInput = _inputTypes[_currentInput];
        InputType? pressedInput = null;

        if (_moveDownAction.WasPerformedThisFrame())
        {
            pressedInput = InputType.ArrowDown;
        }
        else if (_moveUpAction.WasPerformedThisFrame())
        {
            pressedInput = InputType.ArrowUp;
        }
        else if (_moveLeftAction.WasPerformedThisFrame())
        {
            pressedInput = InputType.ArrowLeft;
        }
        else if (_moveRightAction.WasPerformedThisFrame())
        {
            pressedInput = InputType.ArrowRight;
        }

        if (pressedInput.HasValue)
        {
            if (currentInput != pressedInput.Value)
            {
                OnFail();
                return;
            }

            if (_currentInput + 1 <= _numberOfInputs)
            {
                _inputImages[_currentInput].gameObject.SetActive(false);
                _currentInput++;
            }

            if (_currentInput == _numberOfInputs)
            {
                OnSuccess();
            }
        }
    }

    private void OnFail()
    {
        Debug.Log($"QuickTimeEvent: OnFail");
        Clean(false);
        
    }

    private void OnSuccess()
    {
        Debug.Log($"QuickTimeEvent: OnSuccess");
        Clean(true);
    }

    private void Clean(bool success)
    {
        for (var i = _inputImages.Count - 1; i >= 0; i--)
        {
            Destroy(_inputImages[i].gameObject);
        }
        
        _inputImages.Clear();
        _inputTypes.Clear();
        
        _foodStore.OnQuickTimeEventFinished(success);
        
        gameObject.SetActive(false);
    }
}
