using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem.Utilities;

public class PuddleSpread : MonoBehaviour
{
    [SerializeField]
    private float startSize = 0f;
    [SerializeField]
    private float endSize = 1f;
    [SerializeField]
    private float speed = .5f;
    private Vector3 vectorSize;

    private void Awake()
    {
        transform.localScale = new Vector3(startSize, startSize, startSize);
        vectorSize = new Vector3(endSize, endSize, endSize);
    }

    private void Update()
    {
        if ((transform.localScale - vectorSize).magnitude > 0.001f) transform.localScale = Vector3.Lerp(transform.localScale, vectorSize, speed * Time.deltaTime);
    }

    /*
    private void OnTriggerEnter(Collider collision)
    {
        Debug.Log("slip slip");

        if (collision.tag == "Enemy")
        {
            collision.gameObject.GetComponent<NavMeshAgent>().speed = 1f;
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.tag == "Enemy")
        {
            collision.gameObject.GetComponent<NavMeshAgent>().speed = 5f;
        }
    } */

    //for if isTrigger is disabled
    
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("slip slip");

        if (collision.gameObject.tag == "Enemy")
        {
            collision.gameObject.GetComponent<NavMeshAgent>().speed = 1f;


        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            collision.gameObject.GetComponent<NavMeshAgent>().speed = 5f;
        }
    }
    
}
