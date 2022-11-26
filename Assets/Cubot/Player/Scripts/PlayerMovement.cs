using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float maxSpeed;
    [SerializeField] private float directionChangeSpeed;
    [SerializeField] private float pickupRate;
    [SerializeField] private float slowDownRate;
    [SerializeField] private float jumpHeight;
    
    private Rigidbody _playerPhysics;
    private float _speed;
    private float _movementAxisRaw;
    private float _movementAxis;
    private bool _moving;
    [SerializeField]private bool _jumping; // DEBUG
    
    private void Start()
    {
        _playerPhysics = GetComponent<Rigidbody>();
        InputManager.PlayerActions.Movement.performed += Move;
        InputManager.PlayerActions.Movement.canceled += Stop;
        
        InputManager.PlayerActions.Jump.performed += Jump;
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

    private void Jump(InputAction.CallbackContext obj)
    {
        _playerPhysics.AddForce(transform.up * jumpHeight);
        _jumping = true;
    }

    private void Update()
    {
        if (_jumping) return;
        SetupVelocity();
        SetupDirection();
    }

    private void SetupVelocity()
    {
        var vel = transform.TransformDirection(_playerPhysics.velocity);
        
        if (Physics.gravity.y > 0f || Physics.gravity.x != 0f)
            vel.x = -_movementAxis * _speed;
        else 
            vel.x = -_movementAxis * _speed;
        
        _playerPhysics.velocity = transform.InverseTransformDirection(vel);
    }

    private void SetupDirection()
    {
        _movementAxis = Mathf.Lerp(_movementAxis, _movementAxisRaw, directionChangeSpeed);
        
        if (_speed < maxSpeed && _moving) _speed += pickupRate;
        else if (_speed > 0f && !_moving) _speed -= slowDownRate;
    }
}
