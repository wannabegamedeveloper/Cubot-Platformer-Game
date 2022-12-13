using Cinemachine;
using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{
    private static CinemachineVirtualCamera _cvCam;
    private static float _timer;
    
    private void Start()
    {
        _cvCam = GetComponent<CinemachineVirtualCamera>();
    }

    public static void CameraShake(float intensity, float time)
    {
        if (PlayerPrefs.GetInt("CameraShake") == 1) return;
        var cinemachineBasicMultiChannelPerlin =
            _cvCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = intensity;
        _timer = time;
    }

    private void Update()
    {
        if (PlayerPrefs.GetInt("CameraShake") == 1) return;
        if (!(_timer > 0)) return;
        _timer -= Time.deltaTime;
        if (_timer <= 0)
            CameraShake(0f, _timer);
    }
}
