using UnityEngine;

// Make the head rotate to the same direction as the camera without requiring the head to be parented to cam.
// This is essential because the player cameras float in front of their heads to avoid the player being able to see into their body.
// If heads were parented to the cameras, their native pivot points would not be respected (and so heads would be flying around.)
public class CharacterHeadRotation : MonoBehaviour
{
    public GameObject cam;
    
    void Update()
    {
        this.transform.rotation = Quaternion.Euler(cam.transform.rotation.eulerAngles.x - 90.0f, cam.transform.rotation.eulerAngles.y, 0);
    }
}
