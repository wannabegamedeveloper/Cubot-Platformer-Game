using System;
using UnityEngine;

public class Rotating : MonoBehaviour
{
    [SerializeField] private float rotatingSpeed;
    [SerializeField] private Vector3 direction;
    
    private void Update()
    {
        transform.Rotate(transform.parent.forward, rotatingSpeed);
    }
}
