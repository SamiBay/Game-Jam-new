using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class WayPointMoverastronomymoth : MonoBehaviour
{
    public Dialogue dialogue;
    DialogueTrigger dialogueTrigger;

    [SerializeField] private WayPoint waypoint;
    [SerializeField] private float moveSpeed = 3f;
    [SerializeField] private float distanceThresold = 0.1f;
    [SerializeField] private Transform LookAtMe;
    [SerializeField] private GameObject[] gameObjects;
    [SerializeField] private GameObject[] DialogueObjects;
    [SerializeField] private GameObject[] CanvasDialogueObjects;
    [SerializeField]
    private InputActionAsset InputActions;
    private InputActionMap PlayerActionMap;
    private InputAction Enter;

    private Transform currentWaypoint;
    bool Pelaaja = false;
    bool NewDialogue;
    bool nextSentance;
    bool yesno;
    Animator catFishanim;

    // Start is called before the first frame update
    void Start()
    {
        catFishanim = GetComponent<Animator>();
        catFishanim.SetBool("IsWalking", true);

        currentWaypoint = waypoint.GetNextWaypoint(currentWaypoint);
        transform.position = currentWaypoint.position;

        //Set the next waypoint target
        currentWaypoint = waypoint.GetNextWaypoint(currentWaypoint);
        transform.LookAt(currentWaypoint);

        PlayerActionMap = InputActions.FindActionMap("CharacterControls");
        Enter = PlayerActionMap.FindAction("Enter");
        Enter.started += StartEnter;
        Enter.canceled += CancelEnter;

        Enter.Enable();
    }

    private void StartEnter(InputAction.CallbackContext Context)
    {
        if (!NewDialogue && nextSentance)
        {
            FindObjectOfType<DialogueTrigger>().NextSentance();
        }

        if (NewDialogue && yesno)
        {
            FindObjectOfType<DialogueTrigger>().newDialogue();
            NewDialogue = false;
            nextSentance = true;
            CanvasDialogueObjects[0].SetActive(false); //no
            CanvasDialogueObjects[1].SetActive(false); //yes
            yesno = false;
        }
    }
    private void CancelEnter(InputAction.CallbackContext Context)
    {
        if (Pelaaja)
        {

            FindObjectOfType<DialogueManager>().StartDialogue(dialogue);



            gameObjects[0].SetActive(false);    // exclamation mark (AImark)
            Pelaaja = false;
            NewDialogue = true;
            CanvasDialogueObjects[1].SetActive(true);
            DialogueObjects[1].SetActive(true); // yesdialogue
            yesno = true;
        }
    }
    // Update is called once per frame
    void Update()
    {
        if(yesno && Input.GetKeyDown("down"))
        {
            CanvasDialogueObjects[0].SetActive(true); // no
            CanvasDialogueObjects[1].SetActive(false); // yes
            DialogueObjects[0].SetActive(true); // nodialogue
            DialogueObjects[1].SetActive(false); // yesdialogue
        }
        if (yesno && Input.GetKeyDown("up"))
        {
            CanvasDialogueObjects[0].SetActive(false); //no
            CanvasDialogueObjects[1].SetActive(true); //yes
            DialogueObjects[0].SetActive(false); // nodialogue
            DialogueObjects[1].SetActive(true); // yesdialogue
        }



        transform.position = Vector3.MoveTowards(transform.position, currentWaypoint.position, moveSpeed * Time.deltaTime);
        if (Vector3.Distance(transform.position, currentWaypoint.position) < distanceThresold)
        {
            currentWaypoint = waypoint.GetNextWaypoint(currentWaypoint);
            transform.LookAt(currentWaypoint);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {           
            catFishanim.SetBool("IsWalking", false);
            Pelaaja = true;
            moveSpeed = 0;
            gameObjects[0].SetActive(true);     // exclamation mark (AImark)
            
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            Pelaaja = false;
            transform.LookAt(currentWaypoint);          
            moveSpeed = 1;
            gameObjects[0].SetActive(false);         
            catFishanim.SetBool("IsWalking", true);
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            transform.LookAt(LookAtMe);
        }
    }

    public void DialogueEnd()
    {
        yesno = false;
        NewDialogue = false;
        nextSentance = false;
        DialogueObjects[0].SetActive(false); // nodialogue
        DialogueObjects[1].SetActive(false); // yesdialogue
    }

    



}
