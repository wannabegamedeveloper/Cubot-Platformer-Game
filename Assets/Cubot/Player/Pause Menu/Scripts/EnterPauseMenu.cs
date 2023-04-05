using Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class EnterPauseMenu : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera v1;
    [SerializeField] private CinemachineVirtualCamera v2;
    [SerializeField] private Transform player;
    [SerializeField] private LookAtMouse core;
    [SerializeField] private Animator pauseMenu;
    
    private void Start()
    {
        InputManager.PlayerActions.PauseMenu.performed += Menu;
    }

    private void Menu(InputAction.CallbackContext obj)
    {
        if (v1.Priority > v2.Priority)
            OpenMenu();
        else
            CloseMenu();
    }

    private void OpenMenu()
    {
        v1.Priority = v2.Priority - 1;
        player.GetComponent<PlayerMovement>().enabled = false;
        player.GetComponent<Jumping>().paused = true;
        player.GetComponent<GravityMechanic>().paused = true;
        core.enabled = true;
        pauseMenu.Play("ShowPauseMenu", -1, 0f);

        if (PlayerPrefs.GetInt("SFX") == 0)
        {
            GameObject.FindGameObjectsWithTag("Tutorial")[0].GetComponent<Image>().enabled = false;
            GameObject.FindGameObjectsWithTag("Tutorial")[1].GetComponent<Image>().enabled = false;
        }
        
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        var affectedBodies = player.GetComponent<GravityMechanic>().affectedBodies;
        
        for (int i = 0; i < affectedBodies.Count; i++)
            if (i < affectedBodies.Count - 1)
                affectedBodies[i].gameObject.SetActive(false);
    }
    
    public void CloseMenu()
    {
        v2.Priority = v1.Priority - 1;
        player.GetComponent<PlayerMovement>().enabled = true;
        player.GetComponent<Jumping>().paused = false;
        player.GetComponent<GravityMechanic>().paused = false;
        core.enabled = false;
        pauseMenu.Play("HidePauseMenu", -1, 0f);

        if (PlayerPrefs.GetInt("SFX") == 0)
        {
            GameObject.FindGameObjectsWithTag("Tutorial")[0].GetComponent<Image>().enabled = true;
            GameObject.FindGameObjectsWithTag("Tutorial")[1].GetComponent<Image>().enabled = true;
        }

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        var affectedBodies = player.GetComponent<GravityMechanic>().affectedBodies;
        
        for (int i = 0; i < affectedBodies.Count; i++)
            if (i < affectedBodies.Count - 1)
                affectedBodies[i].gameObject.SetActive(true);
    }
}
