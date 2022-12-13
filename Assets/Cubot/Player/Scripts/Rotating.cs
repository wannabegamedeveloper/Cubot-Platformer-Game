using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Rotating : MonoBehaviour
{
    [SerializeField] private float rotatingSpeed;
    [SerializeField] private Vector3 direction;

    private void Start()
    {
        if (SceneManager.GetActiveScene().buildIndex != 0)
            direction = transform.parent.forward;
    }

    private void Update()
    {
        transform.Rotate(direction, rotatingSpeed);
    }
}
