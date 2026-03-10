using UnityEngine;

public class CharacterHeadRotation : MonoBehaviour
{
    public GameObject cam;
    
    void Update()
    {
        this.transform.rotation = Quaternion.Euler(cam.transform.rotation.eulerAngles.x - 90.0f, cam.transform.rotation.eulerAngles.y, 0);
    }
}
