using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class GravityMechanic : MonoBehaviour
{
    private void Start()
    {
        InputManager.PlayerActions.ChangeGravity.performed += ChangeDirection;
    }

    private void ChangeDirection(InputAction.CallbackContext obj)
    {
        Physics.gravity = obj.ReadValue<Vector2>() * 9.81f;
    }
}
