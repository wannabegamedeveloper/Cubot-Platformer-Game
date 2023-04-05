using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private Toggle cameraShake;
    [SerializeField] private Toggle sfx;
    [SerializeField] private EnterPauseMenu enterPauseMenu;

    private GameObject[] _tutorialUI;

    private void Awake()
    {
        _tutorialUI = GameObject.FindGameObjectsWithTag("Tutorial");
        if (PlayerPrefs.GetInt("SFX") == 1)
        {
            sfx.isOn = false;
            _tutorialUI[0].transform.parent.gameObject.SetActive(false);
            _tutorialUI[1].transform.parent.gameObject.SetActive(false);
        }
        else
        {
            sfx.isOn = true;
            _tutorialUI[0].transform.parent.gameObject.SetActive(true);
            _tutorialUI[1].transform.parent.gameObject.SetActive(true);
        }
    }

    private void Start()
    {
        cameraShake.isOn = PlayerPrefs.GetInt("CameraShake") == 0;
        sfx.isOn = PlayerPrefs.GetInt("SFX") == 0;
    }

    public void CloseMenu()
    {
        enterPauseMenu.CloseMenu();
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }
    
    public void ChangeCameraShake()
    {
        PlayerPrefs.SetInt("CameraShake", cameraShake.isOn ? 0 : 1);
    }

    public void ChangeSFX()
    {
        PlayerPrefs.SetInt("SFX", sfx.isOn ? 0 : 1);
        if (PlayerPrefs.GetInt("SFX") == 1)
        {
            _tutorialUI[0].transform.parent.gameObject.SetActive(false);
            _tutorialUI[1].transform.parent.gameObject.SetActive(false);
        }
        else
        {
            _tutorialUI[0].transform.parent.gameObject.SetActive(true);
            _tutorialUI[1].transform.parent.gameObject.SetActive(true);
        }
    }
}
