using System;
using UnityEngine;

public class EnemyGravity : MonoBehaviour
{
    [SerializeField] private float fallMultiplier;
    
    private Rigidbody _enemyPhysics;
    private Transform _transform;

    private void Start()
    {
        _enemyPhysics = GetComponent<Rigidbody>();
        _transform = transform;
    }

    private void Update()
    {
        var localVelocity = transform.InverseTransformDirection(_enemyPhysics.velocity);
        if (localVelocity.y < 0f)
            _enemyPhysics.velocity += _transform.up * -9.81f * (fallMultiplier - 1) * Time.deltaTime;
    }
}
