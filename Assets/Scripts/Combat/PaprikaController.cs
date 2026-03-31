using UnityEngine;

public class PaprikaController : MonoBehaviour
{
    public float lifespan; // paprika will despawn after this many seconds
    public float timeOfCreation;
    public float triggerRadius; // If player appears within this radius, paprika collider becomes trigger to cause damage.
    //public MeshCollider childCollider;
    public GameObject paprika;
    private GameObject player;
    void Awake()
    {
        timeOfCreation = Time.time;
        player = GameObject.FindGameObjectWithTag("Player"); // probably not very performant...
        //Debug.Log("Player name is " + player.name);
    }

    void Update()
    {
        // Despawn this paprika particle after lifespan seconds.
        if (Time.time >= timeOfCreation + lifespan) Destroy(gameObject);

        // Allow the paprika to inflict damage (and fall through the map) if the player is close to the paprika.
        if (Vector2.Distance(new Vector2(player.transform.position.x, player.transform.position.z), 
                             new Vector2(transform.position.x, transform.position.z)) <= triggerRadius)
        {
            (paprika.GetComponent<MeshCollider>() as Collider).isTrigger = true;
            paprika.GetComponent<CapsuleCollider>().enabled = true;

            // This barely works but it's better than nothing... time for bed
        }
    }
}
