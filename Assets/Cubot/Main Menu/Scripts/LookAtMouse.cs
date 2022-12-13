using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class LookAtMouse : MonoBehaviour
{
    [SerializeField] private float distanceFromCamera;

    private void Update()
    {
        if (Camera.main == null) return;
        var mousePosition = Mouse.current.position.ReadValue();

        var mousePosition3D = new Vector3(Screen.width - mousePosition.x, Screen.height - mousePosition.y, -distanceFromCamera);

        var worldPos = Camera.main.ScreenToWorldPoint(mousePosition3D);
        
        transform.LookAt(worldPos);
    }
}
