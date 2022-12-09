using System;
using UnityEngine;

public class CoreFollow : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private float damping;
    
    private Transform _transform;
    
    private void Start()
    {
        _transform = transform;
    }

    private void FixedUpdate()
    {
        _transform.position = Vector3.Lerp(_transform.position, player.position, damping);
        if (Camera.main != null) _transform.LookAt(Camera.main.transform);
    }
}
