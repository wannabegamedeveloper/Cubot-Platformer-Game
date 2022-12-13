using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelsArea : MonoBehaviour
{
    [SerializeField] private List<GameObject> levelButtons;
    [SerializeField] private Toggle cameraShake;
    [SerializeField] private Toggle sfx;
    [SerializeField] private Animator fadingImage;
    
    private void Start()
    {
        for (int i = 0; i < levelButtons.Count; i++)
        {
            if (i < PlayerPrefs.GetInt("CurrentScene"))
                levelButtons[i].SetActive(true);
            else
                levelButtons[i].SetActive(false);
        }

        cameraShake.isOn = PlayerPrefs.GetInt("CameraShake") == 0;
        sfx.isOn = PlayerPrefs.GetInt("SFX") == 0;
    }

    [UsedImplicitly]
    public void ChangeSFX()
    {
        PlayerPrefs.SetInt("CameraShake", sfx.isOn ? 0 : 1);
    }

    [UsedImplicitly]
    public void ChangeCameraShake()
    {
        PlayerPrefs.SetInt("CameraShake", cameraShake.isOn ? 0 : 1);
    }

    [UsedImplicitly]
    public void OpenLevel(int index)
    {
        fadingImage.Play("Fade Out", -1, 0f);
        StartCoroutine(Change(index));
        fadingImage.GetComponent<Image>().raycastTarget = true;
    }

    private static IEnumerator Change(int index)
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(index);
    }
}
