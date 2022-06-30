using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PauseMenu : MonoBehaviour
{
    PlayerInputs playerinputactions;
    Animator anim;
    [SerializeField]
    GameObject[] gameObjects;

    int OptionsTrueFalse = 1;
    // Start is called before the first frame update
    void Awake()
    {
        anim = GetComponent<Animator>();
        playerinputactions = new PlayerInputs();

        playerinputactions.CharacterControls.Up.started += SelectUp;
        playerinputactions.CharacterControls.Down.started += SelectDown;
        playerinputactions.CharacterControls.Enter.started += AceoOfSpades;
    }
    void AceoOfSpades(InputAction.CallbackContext context)
    {
        if (OptionsTrueFalse == 1)
        {
            gameObjects[0].SetActive(false); // pausemenu
        }
        if (OptionsTrueFalse == 0)
        {
            gameObjects[0].SetActive(false); // pausemenu
            gameObjects[1].SetActive(true); // options
        }
        if (OptionsTrueFalse == -1)
        {
            Application.Quit();
            print("quit");
        }
    }

    private void OnEnable()
    {
        playerinputactions.CharacterControls.Enable();
    }
    private void OnDisable()
    {
        playerinputactions.CharacterControls.Disable();
    }

    void SelectUp(InputAction.CallbackContext context)
    {
        if (OptionsTrueFalse == 0)
        {
            anim.SetBool("Options", false);
            OptionsTrueFalse = 1;
        }
        if (OptionsTrueFalse == -1)
        {
            anim.SetBool("Quit", false);
            OptionsTrueFalse = 0;
        }
    }
    void SelectDown(InputAction.CallbackContext context)
    {
        if (OptionsTrueFalse == 0)
        {
            anim.SetBool("Quit", true);
            OptionsTrueFalse = -1;
        }
        if (OptionsTrueFalse == 1)
        {
            anim.SetBool("Options", true);
            OptionsTrueFalse = 0;
        }        
    }
}
