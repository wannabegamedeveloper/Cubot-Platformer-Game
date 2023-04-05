using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public bool movable;
    
    [SerializeField] private float maxSpeed;
    [SerializeField] private float directionChangeSpeed;
    [SerializeField] private float pickupRate;
    [SerializeField] private float slowDownRate;
    
    private Rigidbody _playerPhysics;
    private float _speed;
    private float _movementAxisRaw;
    private float _movementAxis;
    private bool _moving;
    
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        
        _playerPhysics = GetComponent<Rigidbody>();
        InputManager.PlayerActions.Movement.performed += Move;
        InputManager.PlayerActions.Movement.canceled += Stop;
    }

    private void Move(InputAction.CallbackContext obj)
    {
        _movementAxisRaw = obj.ReadValue<float>();
        _moving = true;
    }

    private void Stop(InputAction.CallbackContext obj)
    {
        _movementAxisRaw = 0f;
        _moving = false;
    }

    private void Update()
    {
        if (!movable) return;
        SetupVelocity();
        SetupDirection();
    }

    private void SetupVelocity()
    {
        var vel = transform.TransformDirection(_playerPhysics.velocity);
        
        if (Physics.gravity.y > 0f || Physics.gravity.x != 0f)
            vel.x = -_movementAxis * _speed;
        else 
            vel.x = _movementAxis * _speed;
        
        _playerPhysics.velocity = transform.InverseTransformDirection(vel);
    }

    private void SetupDirection()
    {
        _movementAxis = Mathf.Lerp(_movementAxis, _movementAxisRaw, directionChangeSpeed);
        
        if (_speed < maxSpeed && _moving) _speed += pickupRate;
        else if (_speed > 0f && !_moving) _speed -= slowDownRate;
    }
}
