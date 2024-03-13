using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using Random = UnityEngine.Random;

public class MovementSapma : MonoBehaviour
{
    [SerializeField] Vector3 moveDirection = Vector3.zero;
    float rotationX = 0;
    [Header("Stats")]
    public bool CanMove = true;
    public float gravity;
    public LayerMask Walls;
    private bool Jump;
    [Header("Mouse")]
    public bool YInverted;
    public float lookXLimit;
    public float lookSpeed;
    float MouseX;
    float MouseY;
    [Header("Crouch")]
    public bool CrouchPressed;
    public bool inCrouch;
    public float crouchbobValue;
    public float CrouchCamHeight;
    public float crouchHeight;
    public float crouchSpeed;
    [Header("Speed")]
    public float MovementSpeed;
    public float JumpSpeed;
    

    [Header("Components")]
    private CharacterController characterController;
    public Camera playerCamera;
    


    [HideInInspector] public Vector3 startPosition;
    public AudioSource AuSource;
    public float coyotetime = 0.1f;

    public GameObject LHand;
    public GameObject RHand;
    


    public GameObject[] Hands;
    [Space]
    public Transform RSlot;
    public Transform LSlot;
    [Space]

    public Renderer[] renderers;
    public Canvas can;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        startPosition = transform.position;
        characterController = GetComponent<CharacterController>();
    }
    void Update()
    {

        Hareket();

        Crouch();

        if (Input.GetButtonDown("Fire1"))LeftHand();
        if (Input.GetButtonDown("Fire2"))RightHand();

        if (Input.GetButtonDown("ScreenShot")) StartCoroutine("CaptureScreen");
        if (Input.GetButtonDown("R")) RenderChange();
        MouseInput();

        if (Input.GetButtonDown("Jump"))
        {
            JumpPressed();
        }

    }

    void LeftHand()
    {
        LHand.SendMessage("Use");

    }
    IEnumerator CaptureScreen()
    {

        string folderPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "/Sapma/";
        if (!Directory.Exists(folderPath))
        {
            Directory.CreateDirectory(folderPath);
        }
        var screenshotName =
                                "Screenshot_" +
                                DateTime.Now.ToString("dd-MM-yyyy-HH-mm-ss") +
                                ".png";


        ScreenCapture.CaptureScreenshot(Path.Combine(folderPath, screenshotName));
        Debug.Log(folderPath + screenshotName);
        yield return new WaitForEndOfFrame();
        
      

    }
    void RightHand()
    {
        RHand.SendMessage("Use");

    }

    public void RenderChange()
    {
        foreach(Renderer r in renderers)
        {
            r.enabled = !r.enabled;
        }
        can.enabled = !can.enabled;
    }

    public void Randomize()
    {
        MovementSpeed = MovementSpeed * UnityEngine.Random.Range(0.75f, 1.25f);
        JumpSpeed = JumpSpeed * Random.Range(0.75f, 1.25f);
       
        int firsthand = Random.Range(0, Hands.Length);
        LHand = Hands[firsthand];

        int i;

        for (i = Random.Range(0, Hands.Length); i == firsthand; i = Random.Range(0, Hands.Length))
        {

            Debug.Log("Same Hand");

        }


        RHand = Hands[i];
        ToRightHand(RHand);
        ToLeftHand(LHand);

        
    }

    private void ToRightHand(GameObject handobj)
    {
       Vector3 post = new Vector3(RSlot.position.x, handobj.transform.position.y,handobj.transform.position.z);

        handobj.transform.position = post;
        handobj.SetActive(true);

        if (handobj.name == "Hand")
        {
            Vector3 rot = new Vector3(0, -180f, 0);

            handobj.transform.eulerAngles = rot;
        }
    }

    private void ToLeftHand(GameObject handobj)
    {
        Vector3 post = new Vector3(LSlot.position.x, handobj.transform.position.y, handobj.transform.position.z);

        handobj.transform.position = post;
        handobj.SetActive(true);
    }


    public void MouseInput()
    {
        MouseX = Input.GetAxis("Mouse X");
        MouseY = Input.GetAxis("Mouse Y");
    }
    void Hareket()
    {
        float yatayAks = Input.GetAxis("Horizontal");
        float dikeyAks = Input.GetAxis("Vertical");


        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);

        float curSpeedX = CanMove ? (MovementSpeed) * dikeyAks : 0;
        float curSpeedY = CanMove ? (MovementSpeed) * yatayAks : 0;
        float movementDirectionY = moveDirection.y;

        moveDirection = (forward * curSpeedX) + (right * curSpeedY);


        //Jump
        if (Jump && CanMove && GroundCheck() && !Physics.Raycast(characterController.transform.position, characterController.transform.up, 1.5f, Walls))
        {
            //AuSour.PlayOneShot(jumpSound);
            moveDirection.y = JumpSpeed;
            Jump = false;
            coyotetime = 0f;
        }
        else
        {
            moveDirection.y = movementDirectionY;
        }


        // Gravity

        if (!characterController.isGrounded)
        {
            moveDirection.y -= gravity * Time.deltaTime;
        }

        // Moves Character
        characterController.Move(moveDirection * Time.deltaTime);

        // Karakter Yön
        if (CanMove)
        {
            if (YInverted) rotationX += MouseY * lookSpeed * Time.deltaTime;
            else rotationX += -MouseY * lookSpeed * Time.deltaTime;

            rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);

            playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);

            transform.rotation *= Quaternion.Euler(0, MouseX * lookSpeed * Time.deltaTime, 0);
        }


    }




    public void JumpPressed()
    {
        if (GroundCheck()) Jump = true;

    }

    void Crouch()
    {

        if (!inCrouch && !CrouchPressed) return;
        RaycastHit hit;
        if (CrouchPressed)
        {
            inCrouch = true;
        }
        else if (Physics.Raycast(characterController.transform.position, characterController.transform.up, out hit, 1.8f, Walls))
        {
            inCrouch = true;
        }
        else
        {

            inCrouch = false;
        }


        if (inCrouch && characterController.isGrounded)
        {
            characterController.height = crouchHeight;
            characterController.radius = crouchHeight;
            MovementSpeed = crouchSpeed;

        }
        else if (inCrouch && !characterController.isGrounded)
        {
            characterController.height = crouchHeight;
            characterController.radius = crouchHeight;

        }
        else
        {
            characterController.height = 2f;
            characterController.radius = 0.6f;

        }
    }








    bool GroundCheck()
    {

        if (characterController.isGrounded)
        {

            coyotetime = 0.1f;
            return true;
        }
        else if (!characterController.isGrounded && coyotetime >= 0f)
        {


            coyotetime -= Time.deltaTime * 0.5f;
            return true;
        }

        if (!characterController.isGrounded && coyotetime <= 0f)
        {

            return false;
        }
        else
        {
            return false;
        }

    }
}
