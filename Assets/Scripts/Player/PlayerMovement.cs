using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAPI;
using TMPro;
public class PlayerMovement : NetworkBehaviour
{
    [SerializeField] TMP_InputField textChat;
    [SerializeField] private GameObject introduceCode;
    CharacterController characterController;
    public Transform cameraTransform;
    float pitch = 0f;
    private Vector3 playerVelocity;
    private bool groundedPlayer;
    public float speed = 6.0F;
    public float jumpSpeed = 8.0F;
    public float gravity = 20.0F;
    private Vector3 moveDirection = Vector3.zero;
    void Start()
    {
        if(!IsLocalPlayer){
            cameraTransform.GetComponent<AudioListener>().enabled = false;
            cameraTransform.GetComponent<Camera>().enabled = false;
        }else{
            characterController = GetComponent<CharacterController>();
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if(IsLocalPlayer){
            if(introduceCode.activeSelf != true && !textChat.isFocused){
                MovePlayer();
                Look();

            }

            /*if (Input.GetButtonDown("Jump") && characterController.isGrounded)
            {
             Jump();     
            }*/
        }
        
    }
    void Jump(){
       // playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
       // playerVelocity.y += gravityValue * Time.deltaTime;
       // characterController.Move(playerVelocity * Time.deltaTime);
    }

    void MovePlayer(){
        if (characterController.isGrounded) {
            moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection *= speed;
            if (Input.GetButton("Jump"))
                moveDirection.y = jumpSpeed;

        }
        moveDirection.y -= gravity * Time.deltaTime;
        characterController.Move(moveDirection * Time.deltaTime);
    }

   

   void Look(){
        float mousex = Input.GetAxis("Mouse X") * 3f;
        transform.Rotate(0, mousex, 0);
        pitch -= Input.GetAxis("Mouse Y") * 3f;
        pitch = Mathf.Clamp(pitch, -45f, 45f);
        cameraTransform.localRotation = Quaternion.Euler(pitch, 0, 0);
    }
}
