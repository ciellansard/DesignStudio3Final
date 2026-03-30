using System.Globalization;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR;
using UnityEngine.Rendering.Universal;

public class VrPlayerController : MonoBehaviour
{
    private float gravity = -9.81f;

    public CharacterController controller;


    private AttackControl attackControl;
    private Rigidbody rb;
    private Vector3 playerGravity;

    private bool groundedPlayer = true;
    private GameObject[] enemies;

    public InputActionProperty leftTriggerAction;
    public InputActionProperty rightTriggerAction;

    private void Awake()
    {
        //changes spawnpoint from 0,0,0 to counter
        /*
        controller.enabled = false;
        controller.transform.position = new Vector3(-200, 50, 10);
        controller.transform.rotation = Quaternion.Euler(0, 160, 0);
        controller.enabled = true;
        */

        attackControl = GetComponent<AttackControl>();
        rb = GetComponent<Rigidbody>();

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
        /*
        if (!IsOwner)
        {
            return;
        }
        */

        groundedPlayer = controller.isGrounded;

        if (groundedPlayer)
        {
            // Slight downward velocity to keep grounded stable
            if (playerGravity.y < -2f)
                playerGravity.y = -2f;
        }


        

        //gravity
        playerGravity.y += gravity * Time.deltaTime;


        // Execute main or secondary attack when left or right trigger is pressed.
        if (leftTriggerAction.action != null)
        {
            float leftTrigger = leftTriggerAction.action.ReadValue<float>();
            if (leftTrigger > 0.8f) attackControl.Attack(false, attackControl.entityType);
        }

        if (rightTriggerAction.action != null)
        {
            float rightTrigger = rightTriggerAction.action.ReadValue<float>();
            if (rightTrigger > 0.8f) attackControl.Attack(true, attackControl.entityType);
        }
       
        //Debug.Log($"L: {leftTrigger} | R: {rightTrigger}");
    }
}
