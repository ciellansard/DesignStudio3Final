using System.Linq;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering.Universal;
using UnityEngine.UIElements;
using static UnityEditor.Experimental.GraphView.GraphView;
using static UnityEngine.XR.Interaction.Toolkit.Inputs.Haptics.HapticsUtility;

public class PlayerController : NetworkBehaviour
{
    public float speed = 5f;
    public float mouseSensitivity = 2f;
    public float jumpForce = 1.5f;
    private float gravity = -9.81f;

    public CharacterController controller;
    public Transform cameraTransform;

    public Camera playerCamera;

    float xRotation = 0f;

    private AttackControl attackControl;
    private Rigidbody rb;
    private Vector3 playerGravity;
    
    private bool groundedPlayer = true;
    private GameObject[] enemies;

    public override void OnNetworkSpawn()
    {
        //changes spawnpoint from 0,0,0 to counter
        controller.enabled = false;
        controller.transform.position = new Vector3(-200, 50, 10);
        controller.transform.rotation = Quaternion.Euler(0, 160, 0);
        controller.enabled = true;

        if (!IsOwner)
        {
            //turning off all camera components, without turning off the gameobject as things are parented to it
            playerCamera.gameObject.GetComponent<Camera>().enabled = false;
            playerCamera.gameObject.GetComponent<AudioListener>().enabled = false;
            playerCamera.gameObject.GetComponent<UniversalAdditionalCameraData>().enabled = false;
        }
        else
        {
            attackControl = GetComponent<AttackControl>();
            rb = GetComponent<Rigidbody>();

        }

        UnityEngine.Cursor.lockState = CursorLockMode.Locked;
        UnityEngine.Cursor.visible = false;

        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemies)
        {
            enemy.GetComponent<EnemyAI>().UpdatePlayerList();
        }
    }

  

    // Update is called once per frame
    void Update()
    {
        

        if (!IsOwner)
        {
            return;
        }

        groundedPlayer = controller.isGrounded;

        if (groundedPlayer)
        {
            // Slight downward velocity to keep grounded stable
            if (playerGravity.y < -2f)
                playerGravity.y = -2f;
        }


        Vector2 moveInput = Keyboard.current != null ? new Vector2 
            (
                (Keyboard.current.aKey.isPressed ? -1 : 0) + (Keyboard.current.dKey.isPressed ? 1 : 0),
                (Keyboard.current.sKey.isPressed ? -1 : 0) + (Keyboard.current.wKey.isPressed ? 1 : 0)
            ) : Vector2.zero;   
        
        //movement
        Vector3 move = transform.right * moveInput.x + transform.forward * moveInput.y + transform.up * playerGravity.y;
        controller.Move(move * speed * Time.deltaTime);

        //gravity
        playerGravity.y += gravity * Time.deltaTime;

        //looking around
        Vector2 mouseDelta = Mouse.current.delta.ReadValue();

        float mouseX = mouseDelta.x * mouseSensitivity * Time.deltaTime;
        float mouseY = mouseDelta.y * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -80f, 80f);

        cameraTransform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);

        //jumping
        // referenced https://docs.unity3d.com/ScriptReference/CharacterController.Move.html 
        /*if (Keyboard.current.spaceKey.isPressed && groundedPlayer)
        {
            //Debug.Log("jumping");
            playerGravity.y = Mathf.Sqrt(jumpForce * -2f * gravity);         
        }*/

        if (Keyboard.current.eKey.isPressed) attackControl.Attack();
    }

}
