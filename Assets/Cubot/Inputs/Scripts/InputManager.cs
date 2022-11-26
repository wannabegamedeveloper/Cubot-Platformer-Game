using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static MainPlayer.PlayerActions PlayerActions;
    
    private MainPlayer _mainPlayer;

    private void Awake()
    {
        _mainPlayer = new MainPlayer();
        PlayerActions = _mainPlayer.Player;
    }

    private void OnEnable()
    {
        _mainPlayer.Enable();
    }

    private void OnDestroy()
    {
        _mainPlayer.Disable();
    }
}
