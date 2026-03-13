using UnityEngine;

public class PaprikaController : MonoBehaviour
{
    public float lifespan; // paprika will despawn after this many seconds
    public float timeOfCreation;

    void Awake()
    {
        timeOfCreation = Time.time;
    }

    // Despawn this paprika particle after lifespan seconds.
    void Update()
    {
        if (Time.time >= timeOfCreation + lifespan) Destroy(gameObject);
    }
}
