//using UnityEngine;
//using UnityEngine.InputSystem;
//public class OptionsMenu : MonoBehaviour
//{
//    PlayerInputs playerinputactions;
//    Animator anim;
//    [SerializeField]
//    GameObject[] gameObjects;
//
//    int OptionsTrueFalse = 1;
//    // Start is called before the first frame update
//    void Awake()
//    {
//        anim = GetComponent<Animator>();
//        playerinputactions = new PlayerInputs();
//
//        playerinputactions.CharacterControls.Up.started += SelectUp;
//        playerinputactions.CharacterControls.Down.started += SelectDown;
//        playerinputactions.CharacterControls.Enter.started += AceoOfSpades;
//    }
//    void AceoOfSpades(InputAction.CallbackContext context)
//    {
//        if (OptionsTrueFalse == 1)
//        {
//            gameObjects[0].SetActive(false); // pausemenu
//            gameObjects[1].SetActive(true);
//        }
//        if (OptionsTrueFalse == 0)
//        {
//            
//        }
//        if (OptionsTrueFalse == -1)
//        {
//            
//        }
//    }
//
//    private void OnEnable()
//    {
//        playerinputactions.CharacterControls.Enable();
//    }
//    private void OnDisable()
//    {
//        playerinputactions.CharacterControls.Disable();
//    }
//
//    void SelectUp(InputAction.CallbackContext context)
//    {
//        if (OptionsTrueFalse == 0)
//        {
//            anim.SetBool("", false);
//            OptionsTrueFalse = 1;
//        }
//        if (OptionsTrueFalse == -1)
//        {
//            anim.SetBool("", false);
//            OptionsTrueFalse = 0;
//        }
//        if (OptionsTrueFalse == -2)
//        {
//            anim.SetBool("", );
//            OptionsTrueFalse = -1;
//        }
//    }
//    void SelectDown(InputAction.CallbackContext context)
//    {
//        if (OptionsTrueFalse == -1)
//        {
//            anim.SetBool("", );
//            OptionsTrueFalse = -2;
//        }
//        if (OptionsTrueFalse == 0)
//        {
//            anim.SetBool("", true);
//            OptionsTrueFalse = -1;
//        }
//        if (OptionsTrueFalse == 1)
//        {
//            anim.SetBool("", true);
//            OptionsTrueFalse = 0;
//        }
//        
//    }
//}
