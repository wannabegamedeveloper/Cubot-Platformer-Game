using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Jumping : MonoBehaviour
{
    [SerializeField] private float jumpVelocity;
    [SerializeField] private float rayDistance;
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private float fallMultiplier;
    [SerializeField] private float lowJumpMultiplier;
    [SerializeField] private float highJumpMultiplier;
    
    private Rigidbody _playerPhysics;
    private Transform _transform;
    private bool _jumpPressed;
    
    private void Start()
    {
        _transform = transform;
        _playerPhysics = GetComponent<Rigidbody>();
        InputManager.PlayerActions.Jump.performed += Jump;
        InputManager.PlayerActions.Jump.canceled += JumpLeft;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.transform.CompareTag("Platform")) return;
        var ray = new Ray(_transform.position, -_transform.up);
        if (Physics.Raycast(ray, rayDistance))
            playerMovement.movable = true;
    }

    private void Update()
    {
        var localVelocity = transform.InverseTransformDirection(_playerPhysics.velocity);
        if (localVelocity.y < 0f)
            _playerPhysics.velocity += _transform.up * -9.81f * (fallMultiplier - 1) * Time.deltaTime;
        else if (localVelocity.y > 0 && !_jumpPressed)
            _playerPhysics.velocity += _transform.up * -9.81f * (lowJumpMultiplier - 1) * Time.deltaTime;
        else if (localVelocity.y > 0 && _jumpPressed)
            _playerPhysics.velocity += _transform.up * -9.81f * (highJumpMultiplier - 1) * Time.deltaTime;
    }

    private void OnDrawGizmos()  //
    {
        var transform1 = transform;  //
        var position = transform1.position;  //
        Gizmos.DrawLine(position, position - transform1.up * rayDistance);   //  DEBUG
    }

    private void Jump(InputAction.CallbackContext obj)
    {
        if (!playerMovement.movable) return;
        _playerPhysics.AddForce(_transform.up * jumpVelocity * 100f);
        playerMovement.movable = false;
        _jumpPressed = true;
    }

    private void JumpLeft(InputAction.CallbackContext obj)
    {
        _jumpPressed = false;
    }
}
