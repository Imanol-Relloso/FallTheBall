using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance { get; private set; }
    
    public event Action<Vector2> OnDragEnd;
    public event Action<Vector2> OnKeyInput;
    
    private Vector2 startPosition;
    private Vector2 _currentPosition;
    private bool _isDragging = false;
    
    private InputAction _contactAction;
    private InputAction _positionAction;
    private InputAction _keyboardAction;
    
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
    
    private void Start()
    {
        PlayerInput playerInput = GetComponent<PlayerInput>();

        var playerMap = playerInput.actions.FindActionMap("Player");

        _contactAction = playerMap.FindAction("PrimaryContact");
        _positionAction = playerMap.FindAction("PrimaryPosition");
        _keyboardAction = playerMap.FindAction("KeyboardMove");
        
        _contactAction.started += OnTouchStart;
        _contactAction.canceled += OnTouchEnd;
        
        _positionAction.performed += OnPositionChanged;
        
        _keyboardAction.started += OnKeyboardInput;
    }

    private void OnKeyboardInput(InputAction.CallbackContext obj)
    {
        OnKeyInput?.Invoke(obj.ReadValue<Vector2>());
    }

    private void OnTouchStart(InputAction.CallbackContext obj)
    {
        _isDragging = true;
        startPosition = _positionAction.ReadValue<Vector2>();
        _currentPosition = startPosition;
    }
    
    private void OnPositionChanged(InputAction.CallbackContext obj)
    {
        if (_isDragging)
            _currentPosition = obj.ReadValue<Vector2>();    }
    
    private void OnTouchEnd(InputAction.CallbackContext obj)
    {
        if (!_isDragging) return;

        _isDragging = false;

        Vector2 dragDirection = _currentPosition - startPosition;

        if (dragDirection.sqrMagnitude > 0)
            dragDirection.Normalize();

        OnDragEnd?.Invoke(dragDirection);    
    }
}
