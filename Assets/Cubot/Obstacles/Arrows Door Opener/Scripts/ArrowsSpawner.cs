using UnityEngine;

public class ArrowsSpawner : MonoBehaviour
{
    public int index;
    
    [SerializeField] private GameObject key;
    [SerializeField] private GameObject arrow;
    [SerializeField] private GameObject door;
    [SerializeField] private Direction[] directions;
    [SerializeField] private float distance;

    private int _count;
    
    private enum Direction
    {
        Up = 270, Left = 180, Right = 0, Down = 90
    }

    private void Start()
    {
        _count = directions.Length;
    }

    public void Spawn(Vector3 position)
    {
        if (index == _count)
        {
            door.SetActive(false);
            return;
        }

        var pos = position;
        var rot = Quaternion.identity;
        
        if (directions[index] == Direction.Left)
            pos += Vector3.left * distance;
        else if (directions[index] == Direction.Right)
            pos += Vector3.right * distance;
        else if (directions[index] == Direction.Down)
            pos += Vector3.down * distance;
        else if (directions[index] == Direction.Up)
            pos += Vector3.up * distance;

        if (index == _count - 1)
        {
            var keySphere = Instantiate(key, pos, rot, transform);
            Destroy(keySphere, 1f);
            index++;
            return;
        }
        
        rot = Quaternion.Euler((float) directions[index + 1], 90f, 0f);
        
        Instantiate(arrow, pos, rot, transform);
        
        index++;
    }
}
