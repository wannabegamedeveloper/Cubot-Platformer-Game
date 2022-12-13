using UnityEngine;

public class RotatingKillzone : MonoBehaviour
{
    [SerializeField] private float rotationSpeed;
    
    private Quaternion _rotation;
    
    private void Start()
    {
        _rotation = transform.rotation;
    }

    private void SetDirection()
    {
        _rotation = Quaternion.LookRotation(Physics.gravity, transform.up);
    }

    private void Update()
    {
        transform.rotation = Quaternion.Lerp(transform.rotation, _rotation, rotationSpeed * Time.deltaTime);
    }
}
