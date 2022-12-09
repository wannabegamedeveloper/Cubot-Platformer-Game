using UnityEngine;

public class RotateObject : MonoBehaviour
{
    [SerializeField] private float speed;
    
    private void Update()
    {
        transform.Rotate(Vector3.one * speed * Time.deltaTime);
    }
}
