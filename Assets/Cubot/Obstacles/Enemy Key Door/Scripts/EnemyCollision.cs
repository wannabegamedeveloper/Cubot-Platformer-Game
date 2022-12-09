using System;
using UnityEngine;

public class EnemyCollision : MonoBehaviour
{
    [SerializeField] private Animator door;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Key"))
        {
            var collidedKey = other.transform;
            collidedKey.GetComponent<Animator>().Play("Die 2");
            collidedKey.GetComponent<Collider>().enabled = false;

            var keyParent = collidedKey.parent;
            keyParent.GetComponent<EnemyDoor>().index--;
            keyParent.GetComponent<EnemyDoor>().OpenDoor();
        }
    }
}
