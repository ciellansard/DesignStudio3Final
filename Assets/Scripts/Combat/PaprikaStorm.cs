using UnityEngine;

public class PaprikaStorm : MonoBehaviour
{
    public bool playerSpotted = false;
    public GameObject[] paprikaPrefab;
    public float minTimeBetweenStorms; // In 
    public float maxTimeBetweenStorms;
    public float stormDuration;
    public Vector3 stormCentre;
    public float stormRadius;

    private bool firstStormTimeSet = false;
    private float nextStormTime;
    private float nextPaprikaTime;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (playerSpotted)
        {
            if (!firstStormTimeSet || Time.time > nextStormTime + stormDuration)
            {
                nextStormTime = Time.time + Random.Range(minTimeBetweenStorms, maxTimeBetweenStorms);
                Debug.Log("storm in " + (nextStormTime - Time.time) + " seconds.");
                firstStormTimeSet = true;
            }

            if (Time.time >= nextStormTime && Time.time <= nextStormTime + stormDuration)
            {
                Vector2 paprikaPos = Random.insideUnitCircle;
                Instantiate(paprikaPrefab[(int)Time.time % paprikaPrefab.Length], new Vector3(paprikaPos.x * stormRadius, 20, paprikaPos.y * stormRadius), Quaternion.Euler(Random.Range(0, 20), Random.Range(0, 360), 0));
            }
        }
    }
}
