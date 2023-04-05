using System.Collections;
using Cinemachine;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Transitions : MonoBehaviour
{
    [SerializeField] private Animator mainMenu;
    [SerializeField] private Animator startScreen;
    [SerializeField] private Animator fadingImage;
    [SerializeField] private GameObject continueButton;
    [SerializeField] private CinemachineVirtualCamera v1;
    [SerializeField] private CinemachineVirtualCamera v2;
    
    private bool _mainMenuOpen;
    private bool _interactableButtons;
    
    private void Awake()
    {
        InputSystem.onAnyButtonPress.Call(_ => PressedKey());
    }

    private void Start()
    {
        if (PlayerPrefs.GetInt("CurrentScene") == 0)
            continueButton.SetActive(false);
    }

    [UsedImplicitly]
    public void ContinueGame()
    {
        if (!_interactableButtons) return;
        ChangeScene(PlayerPrefs.GetInt("CurrentScene"));
    }

    [UsedImplicitly]
    public void NewGame()
    {
        if (!_interactableButtons) return;
        PlayerPrefs.DeleteAll();
        ChangeScene(1);
    }

    [UsedImplicitly]
    public void Levels()
    {
        if (!_interactableButtons) return;
        v1.Priority = v2.Priority - 1;
    }

    [UsedImplicitly]
    public void Back()
    {
        if (!_interactableButtons) return;
        v2.Priority = v1.Priority - 1;
    }

    [UsedImplicitly]
    public void Quit()
    {
        if (!_interactableButtons) return;
        Application.Quit();
    }

    [UsedImplicitly]
    public void OpenItch()
    {
        if (!_interactableButtons) return;
        Application.OpenURL("https://loopinteractive.itch.io/");
    }

    [UsedImplicitly]
    public void OpenArt()
    {
        if (!_interactableButtons) return;
        Application.OpenURL("https://www.artstation.com/tusharvaid30");
    }

    [UsedImplicitly]
    public void OpenInsta()
    {
        if (!_interactableButtons) return;
        Application.OpenURL("https://www.instagram.com/loop_interactive/");
    }
    
    private void ChangeScene(int sceneIndex)
    {
        fadingImage.Play("Fade Out", -1, 0f);
        StartCoroutine(Change(sceneIndex));
    }

    private static IEnumerator Change(int index)
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(index);
    }
    
    private void PressedKey()
    {
        if (_mainMenuOpen) return;
        fadingImage.GetComponent<Image>().raycastTarget = false;
        startScreen.Play("Start Screen", -1, 0f);
        _mainMenuOpen = true;
    }

    [UsedImplicitly]
    private void OpenMenu()
    {
        if (PlayerPrefs.GetInt("CurrentScene") != 0)
            mainMenu.Play("Show Main Menu", -1, 0f);
        else
            mainMenu.Play("Show Main Menu 2", -1, 0f);
    }

    [UsedImplicitly]
    private void InteractableButtons()
    {
        _interactableButtons = true;
    }
}
