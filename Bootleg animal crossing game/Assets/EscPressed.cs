using UnityEngine;
using UnityEngine.InputSystem;

public class EscPressed : MonoBehaviour
{
    PlayerInputs playerinputactions;
    public GameObject pauseMenu;
    bool GamePause;
    // Start is called before the first frame update
    void Awake()
    {
        playerinputactions = new PlayerInputs();
        playerinputactions.CharacterControls.Esc.started += Escape;
    }
    private void OnEnable()
    {
        playerinputactions.CharacterControls.Enable();
    }
    private void OnDisable()
    {
        playerinputactions.CharacterControls.Disable();
    }

    void Escape(InputAction.CallbackContext context)
    {
        if (GamePause)
        {
            Resume();
        }
        else
        {
            Pause();
        }
    }
    void Pause()
    {
        pauseMenu.SetActive(true);
       // Time.timeScale = 0f;
        GamePause = true;
    }
    void Resume()
    {
        pauseMenu.SetActive(false);
       // Time.timeScale = 1f;
        GamePause = false;
    }
}
