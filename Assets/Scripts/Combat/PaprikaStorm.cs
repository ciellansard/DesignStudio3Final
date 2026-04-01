using UnityEngine;

public class PaprikaStorm : MonoBehaviour
{
    public bool playerSpotted = false;

    public GameObject[] paprikaPrefab;
    public float minTimeBetweenStorms;
    public float maxTimeBetweenStorms;
    public float stormDuration;
    public Vector3 stormCentre;
    public float stormRadius;

    private bool firstStormTimeSet = false;
    private float nextStormTime;
    private float nextPaprikaTime;

    private SoundManager soundManager;

    private void Awake()
    {
        soundManager = GameObject.FindGameObjectWithTag("SoundManager").GetComponent<SoundManager>();
    }

    void Update()
    {
        if (playerSpotted)
        {
            // If no storms have happened yet, start one now.
            if (!firstStormTimeSet)
            {
                nextStormTime = Time.time;
                firstStormTimeSet = true;
            }

            // If the storm has ended, choose a time for the next storm.
            if (Time.time > nextStormTime + stormDuration)
            {
                nextStormTime = Time.time + Random.Range(minTimeBetweenStorms, maxTimeBetweenStorms);
                //Debug.Log("storm in " + (nextStormTime - Time.time) + " seconds.");
            }

            // From nextStormTime to nextStormTime + stormDuration, spawn paprika particles above the arena.
            // They have random rotations on them just for visual variation.
            if (Time.time >= nextStormTime && Time.time <= nextStormTime + stormDuration)
            {
                soundManager.PlaySound(soundManager.thunder);
                //soundManager.FadeOutSfx(2);
                Vector2 paprikaPos = Random.insideUnitCircle;
                Instantiate(paprikaPrefab[((int)(Time.time * 100)) % paprikaPrefab.Length], new Vector3(paprikaPos.x * stormRadius, stormCentre.y, paprikaPos.y * stormRadius), Quaternion.Euler(Random.Range(0, 20), Random.Range(0, 360), 0));
            }
        }
    }
}
