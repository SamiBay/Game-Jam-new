using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;
using Cinemachine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

[RequireComponent(typeof(NavMeshAgent))]

public class NavMeshMovement : MonoBehaviour
{
    [SerializeField] private Transform LookAtMe;

    bool spacePressed;
    bool Fishing;
    int pullFish = 0;

    [SerializeField] CinemachineVirtualCamera PlayerCam;
    [SerializeField] CinemachineVirtualCamera MajakkaCam;
    [SerializeField] CinemachineVirtualCamera LibraryCam;

    //public GameObject cube;

    bool LightHouseDoor;
    bool WorldSceneDoor;
    bool FroggyHouseDoor;
    bool DuckieHouseDoor;
    bool Library;

    public GameObject loadingscreen;
    public Slider slider;


    [SerializeField]
    private InputActionAsset InputActions;
    private InputActionMap PlayerActionMap;
    private InputAction Movement;
    private InputAction Run;
    private InputAction Enter;

    [SerializeField]
    private NavMeshAgent Agent;
    [SerializeField]
    [Range(0, 0.99f)]
    private float smoothing = 0.25f;

    [SerializeField]
    private float TargetLerpSpeed = 1;
    private Vector3 TargetDirection;
    private float LerpTime = 0;
    private Vector3 LastDirection;
    private Vector3 MovementVector;

    Animator anim;
    bool isRunPressed;
    bool isWalkPressed;
    bool AI = false;
    bool AIdialogue = false;
    private void Awake()
    {
        anim = GetComponent<Animator>();
        Agent = GetComponent<NavMeshAgent>();
        PlayerActionMap = InputActions.FindActionMap("CharacterControls");
        Movement = PlayerActionMap.FindAction("Move");
        Movement.started += HandleMovementAction;
        Movement.canceled += CancelHandleMovementAction;
        Movement.performed += HandleMovementAction;
        Run = PlayerActionMap.FindAction("Run");
        Run.started += runrun;
        Run.canceled += runrun;
        Enter = PlayerActionMap.FindAction("Enter");
        Enter.started += StartEnter;
        Enter.canceled += CancelEnter;    
    }
    private void Start()
    {
        LoadPlayer();
    }

    private void OnEnable()
    {
        Enter.Enable();
        Run.Enable();
        Movement.Enable();
        PlayerActionMap.Enable();
        InputActions.Enable();
        CameraSwitch.Register(PlayerCam);
        CameraSwitch.Register(MajakkaCam);
        CameraSwitch.Register(LibraryCam);
        CameraSwitch.SwitchCamera(PlayerCam);
    }

    private void OnDisable()
    {
        CameraSwitch.Unregister(PlayerCam);
        CameraSwitch.Unregister(MajakkaCam);
        CameraSwitch.Register(LibraryCam);
    }

    private void CancelEnter(InputAction.CallbackContext Context)
    {
        
        if (AIdialogue == true && AI == false)
        {
            AI = true;
            Agent.speed = 0;
            anim.SetBool("isRunning", false);
            anim.SetBool("isWalking", false);
        }
        if (LightHouseDoor || DuckieHouseDoor || Library || FroggyHouseDoor || WorldSceneDoor)
        {
            OpenSesam();
        }
        if (pullFish == 1)
        {

            anim.SetBool("PullFish", true);
            pullFish = 2;
            
        }
        if (Fishing && !spacePressed)
        {
            transform.LookAt(LookAtMe);
            Agent.speed = 0;
            AI = true;
            pullFish = 1;
            anim.SetBool("Fishing", true);
            spacePressed = true;
            Debug.Log("spacePressed");
        }
       
    }
    
    private void StartEnter(InputAction.CallbackContext Context)
    {

        if(Fishing)
        {

            



            
            //FindObjectOfType<SpawnScript>().Fish();
        }
        
    }

   

    private void CancelHandleMovementAction(InputAction.CallbackContext Context)
    {
        Vector2 input = Context.ReadValue<Vector2>();
        MovementVector = new Vector3(input.x, 0, input.y);
        isWalkPressed = false;
    }
    private void HandleMovementAction(InputAction.CallbackContext Context)
    {
        Vector2 input = Context.ReadValue < Vector2>();
        MovementVector = new Vector3(input.x, 0, input.y);
        isWalkPressed = true;
    }
    private void FixedUpdate()
    {
        
        if (Fishing && pullFish == 2)
        {
            anim.SetBool("Fishing", false);
            pullFish = 0;
            spacePressed = false;
            anim.SetBool("PullFish", false);
            AI = false;
        }

        MovementVector.Normalize();
        if(MovementVector != LastDirection)
        {
            LerpTime = 0;
            
            anim.SetBool("isRunning", false);
            anim.SetBool("isWalking", false);
        }

        LastDirection = MovementVector;
        TargetDirection = Vector3.Lerp(TargetDirection, MovementVector, Mathf.Clamp01(LerpTime * TargetLerpSpeed * (1 - smoothing)));

        Agent.Move(TargetDirection * Agent.speed * Time.deltaTime);
        

        Vector3 lookDirection = MovementVector;

        if (AI == false && lookDirection != Vector3.zero)
        {          
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(lookDirection), Mathf.Clamp01(LerpTime * TargetLerpSpeed * (1 - smoothing)));
        }

        if (AI == false && isRunPressed && !isWalkPressed)
        {
            Agent.speed = 1;
            anim.SetBool("isRunning", true);
            anim.SetBool("isWalking", false);
        }
        if (AI == false && isWalkPressed && !isRunPressed)
        {
            Agent.speed = 1;
            anim.SetBool("isRunning", false);
            anim.SetBool("isWalking", true);
        }

        if (AI == false && !isWalkPressed)
        {
            anim.SetBool("isWalking", false);
        }
        if (AI == false && isWalkPressed)
        {
            Agent.speed = 1;
            anim.SetBool("isWalking", true);
        }

        if (AI == false && isWalkPressed && isRunPressed)
        {
            Agent.speed = 3;
            anim.SetBool("isRunning", true);
            anim.SetBool("isWalking", true);
        }
        if (AI == false && !isRunPressed && !isWalkPressed)
        {
            Agent.speed = 1;
            anim.SetBool("isRunning", false);
            anim.SetBool("isWalking", false);
        }

        LerpTime += Time.deltaTime;
    }
    private void runrun(InputAction.CallbackContext run )
    {       
        {
            isRunPressed = run.ReadValueAsButton();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "FishingArea")
        {
            Debug.Log("fishingtrue");
            Fishing = true;
        }
        if (other.tag == "AI")
        {
            AIdialogue = true;
        }

        //CMshake.Shake.ShakeCam(5f, 1f);
        //print("osuma");
        if (other.tag == "Library")
        {
            SavePlayer();
            Library = true;
        }
        if (other.tag == "WorldSceneDoor")
        {
            SavePlayer();
            WorldSceneDoor = true;
        }
        if (other.tag == "LightHouseDoor")
        {
            SavePlayer();
            LightHouseDoor = true;
            //cube.SetActive(true);
        }
        if (other.tag == "FroggyHouseDoor")
        {
            SavePlayer();
            FroggyHouseDoor = true;
        }
        if (other.tag == "DuckieHouseDoor")
        {
            SavePlayer();
            DuckieHouseDoor = true;
        }

        if (other.tag == "Majakka" && CameraSwitch.IsActiveCamera(PlayerCam))
        {
            CameraSwitch.SwitchCamera(MajakkaCam);
        }
        if (other.tag == "Library" && CameraSwitch.IsActiveCamera(PlayerCam))
        {
            CameraSwitch.SwitchCamera(LibraryCam);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "FishingArea")
        {
            Fishing = false;
        }
        if (other.tag == "Majakka" || other.tag == "Library")
        {
            CameraSwitch.SwitchCamera(PlayerCam);
        }
        if (other.tag == "Library")
        {
            Library = false;
        }
        if (other.tag == "WorldSceneDoor")
        {
            WorldSceneDoor = false;
        }
        if (other.tag == "LightHouseDoor")
        {
            LightHouseDoor = false;
            //cube.SetActive(true);
        }
        if (other.tag == "FroggyHouseDoor")
        {
            FroggyHouseDoor = false;
        }
        if (other.tag == "DuckieHouseDoor")
        {
            DuckieHouseDoor = false;
        }
    }
    public void OpenSesam()
    {
        if (Library)
        {
            Library = false;
            Loadlevel(5);
        }
        if (DuckieHouseDoor)
        {
            DuckieHouseDoor = false;
            Loadlevel(4);
        }
        if (FroggyHouseDoor)
        {
            FroggyHouseDoor = false;
            Loadlevel(3);
        }
        if (LightHouseDoor)
        {
            LightHouseDoor = false;
            Loadlevel(2);
        }
        if (WorldSceneDoor)
        {
            WorldSceneDoor = false;
            Loadlevel(1);
        }
    }
    public void Loadlevel(int sceneIndex)
    {
        StartCoroutine(LoadAsynchronously(sceneIndex));
    }

    IEnumerator LoadAsynchronously(int sceneIndex)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);

        loadingscreen.SetActive(true);

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);

            slider.value = progress;

            yield return null;
        }
    }
    public void SavePlayer()
    {
        SaveSystem.SavePlayer(this);
        print("Data saved");
    }
    public void LoadScene()
    {
        SceneManager.LoadScene(1);
    }

    public void LoadPlayer()
    {
        PlayerData data = SaveSystem.LoadPlayer();

        Vector3 position;
        position.x = data.position[0];
        position.y = data.position[1];
        position.z = data.position[2];

        transform.position = position;
        //print(position);

    }

    public void SpeedNormalized()
    {
        AI = false;
        AIdialogue = false;
    }
}
    