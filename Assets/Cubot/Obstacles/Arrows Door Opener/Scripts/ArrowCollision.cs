using System.Collections;
using JetBrains.Annotations;
using UnityEngine;

public class ArrowCollision : MonoBehaviour
{
    [SerializeField] private float cameraShakeIntensity = 5f;
    [SerializeField] private float cameraShakeTime = .1f;
    
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
            CameraBehaviour.CameraShake(cameraShakeIntensity, cameraShakeTime);

            if (PlayerPrefs.GetInt("SFX") == 0)
                other.GetComponent<AudioSource>().Play();
            
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
        GetComponent<Collider>().enabled = false;
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
