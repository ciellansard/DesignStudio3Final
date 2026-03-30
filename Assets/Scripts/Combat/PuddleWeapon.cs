using UnityEngine;

public class PuddleWeapon : MonoBehaviour
{
    
    [SerializeField]
    private GameObject puddle;

    private void OnTriggerEnter(Collider other)
    {
        float currentY = gameObject.transform.position.y;
        GameObject puddleObject = Instantiate(puddle);
        puddleObject.transform.position = new Vector3(gameObject.transform.position.x, currentY, gameObject.transform.position.z);

        Destroy(gameObject);
    }
}
