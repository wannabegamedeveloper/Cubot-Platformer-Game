using System.Collections;
using JetBrains.Annotations;
using UnityEngine;

public class ArrowCollision : MonoBehaviour
{
    private Transform _transform;
    private Animator _animator;
    
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
            
            var arrowSpawner = _transform.parent.GetComponent<ArrowsSpawner>();

            if (_transform.name == "Start")
                arrowSpawner.index = 0;
            
            arrowSpawner.Spawn(_transform.position);
            if (_transform.name != "Start")
                StartCoroutine(Die());
        }
    }

    private IEnumerator Die()
    {
        yield return new WaitForSeconds(1f);
        _animator.Play("Die", -1, 0f);
    }

    [UsedImplicitly]
    private void DestroyObject()
    {
        if (_transform.name != "Start")
            Destroy(gameObject);
    }
}
