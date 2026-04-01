using UnityEngine;

public class StepsVR : MonoBehaviour
{
    public Camera camera;

    private FootstepManager footstepManager;
    private bool isWalking = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        footstepManager = GetComponent<FootstepManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (camera.velocity.x != 0 || camera.velocity.z != 0) { 
            
         isWalking = true;
            Debug.Log("wALLKKKINGG");
        
        }
        else isWalking = false;

        if (isWalking)
        {
            footstepManager.StartWalking();
        }
        else
        {
            footstepManager.StopWalking();
        }
    }
}
