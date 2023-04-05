using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class KeyInput : MonoBehaviour
{
    [SerializeField] private Animator movement;
    [SerializeField] private Animator grav;
    
    private bool aKey;
    
    private void Start()
    {
        InputManager.PlayerActions.Movement.performed += Move;
        InputManager.PlayerActions.Movement.canceled += Stop;
        InputManager.PlayerActions.ChangeGravity.performed += Gravity;
    }

    private void Gravity(InputAction.CallbackContext obj)
    {
        if (PlayerPrefs.GetInt("SFX") == 1) return;
        var dir = obj.ReadValue<Vector2>();
        if (Mathf.Approximately(dir.x, 1f))
            grav.Play("ToRight", -1, 0f);
        else if (Mathf.Approximately(dir.x, -1f))
            grav.Play("ToLeft", -1, 0f);
        else if (Mathf.Approximately(dir.y, 1f))
            grav.Play("ToUp", -1, 0f);
        else if (Mathf.Approximately(dir.y, -1f))
            grav.Play("ToDown", -1, 0f);
    }

    private void Move(InputAction.CallbackContext obj)
    {
        if (PlayerPrefs.GetInt("SFX") == 1) return;
        float direction = obj.ReadValue<float>();
        if (direction < 0f)
        {
            movement.Play("A Press", -1, 0f);
            aKey = true;
        }
        else if (direction > 0f)
        {
            movement.Play("D Press", -1, 0f);
            aKey = false;
        }
    }

    private void Stop(InputAction.CallbackContext obj)
    {
        if (PlayerPrefs.GetInt("SFX") == 1) return;
        if (aKey)
            movement.Play("A Release", -1, 0f);
        else
            movement.Play("D Release", -1, 0f);
    }
}
