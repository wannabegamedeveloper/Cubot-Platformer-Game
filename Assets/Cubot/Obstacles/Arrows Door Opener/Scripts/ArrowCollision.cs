using System.Collections;
using JetBrains.Annotations;
using UnityEngine;

public class ArrowCollision : MonoBehaviour
{
    private Transform _transform;
    private Animator _animator;
    private ArrowsSpawner _arrowsSpawner;
    
    private void Start()
    {
        _transform = transform;
        _animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _animator.Play("Trigger Rings", -1, 0f);
            
            _arrowsSpawner = _transform.parent.GetComponent<ArrowsSpawner>();
            _transform.GetComponent<BoxCollider>().enabled = false;

            if (_transform.name == "Start")
                _arrowsSpawner.index = 0;

            _arrowsSpawner.Spawn(_transform.position);
        }
    }

    public IEnumerator Die()
    {
        yield return new WaitForSeconds(1f);
        _transform.GetComponent<Collider>().enabled = false;
        _animator.Play("Die", -1, 0f);
    }

    [UsedImplicitly]
    private void DestroyObject()
    {
        if (_transform.name != "Start")
        {
            Destroy(gameObject);
            _arrowsSpawner = _transform.parent.GetComponent<ArrowsSpawner>();
            _arrowsSpawner.spawned--;
            if (_arrowsSpawner.spawned == 0)
                _arrowsSpawner.ResetColliders();
        }
    }
}
