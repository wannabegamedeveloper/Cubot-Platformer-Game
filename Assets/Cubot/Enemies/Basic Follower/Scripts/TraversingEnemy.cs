using System;
using UnityEngine;

public class TraversingEnemy : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float forwardRayDistance;
    [SerializeField] private float groundCheckRayDistance;
    [SerializeField] private LayerMask layersToDetect;
    
    private int _direction = 1;
    private Rigidbody _enemyPhysics;
    private Transform _transform;
    
    private void Start()
    {
        _enemyPhysics = GetComponent<Rigidbody>();
        _transform = transform;
    }

    private void Update()
    {
        if (!Physics.Raycast(_transform.position, -_transform.up, groundCheckRayDistance)) return;
        _enemyPhysics.velocity = _transform.right * _direction * speed;
        if (Physics.Raycast(_transform.position, _transform.right * _direction, forwardRayDistance, layersToDetect) ||
            !Physics.Raycast(_transform.position + _transform.right * _direction, -_transform.up, groundCheckRayDistance))
            _direction *= -1;
    }
}
