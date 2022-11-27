using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class GravityMechanic : MonoBehaviour
{
    private Quaternion _rotationBuffer;
    private Transform _transform;
    
    private void Start()
    {
        _transform = transform;
        InputManager.PlayerActions.ChangeGravity.performed += ChangeDirection;
    }

    private void ChangeDirection(InputAction.CallbackContext obj)
    {
        var dir = obj.ReadValue<Vector2>();
        Physics.gravity = dir * 9.81f;
        
        if (Mathf.Approximately(dir.x, 1f))
            _rotationBuffer = Quaternion.Euler(0f, 0f, 90f);
        else if (Mathf.Approximately(dir.x, -1f))
            _rotationBuffer = Quaternion.Euler(0f, 0f, 270f);
        else if (Mathf.Approximately(dir.y, 1f))
            _rotationBuffer = Quaternion.Euler(0f, 0f, 180f);
        else if (Mathf.Approximately(dir.y, -1f))
            _rotationBuffer = Quaternion.Euler(0f, 0f, 0f);
    }

    private void Update()
    {
        transform.rotation = _rotationBuffer;
    }
}
