using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GravityMechanic : MonoBehaviour
{
    public bool paused;
    
    [SerializeField] public List<Transform> affectedBodies;

    private Quaternion _rotationBuffer;

    private void Start()
    {
        Physics.gravity = Vector3.down * 9.81f;
        InputManager.PlayerActions.ChangeGravity.performed += ChangeDirection;
        affectedBodies.Add(transform);
    }

    private void ChangeDirection(InputAction.CallbackContext obj)
    {
        if (paused) return;
        var oldPhysicsGravity = Physics.gravity;

        foreach (var affectedBody in affectedBodies)
            affectedBody.GetComponent<Rigidbody>().velocity = Vector3.zero;

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
        
        if (oldPhysicsGravity != Physics.gravity)
            GetComponent<PlayerMovement>().movable = false;
        
        transform.rotation = _rotationBuffer;
        GetComponent<Jumping>().CheckGround();
    }

    private void Update()
    {
        if (paused) return;
        transform.rotation = _rotationBuffer;
     
        foreach (var affectedBody in affectedBodies)
            affectedBody.rotation = _rotationBuffer;
    }
}
