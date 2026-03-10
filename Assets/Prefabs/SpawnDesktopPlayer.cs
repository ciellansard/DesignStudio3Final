using UnityEngine;

public class SpawnDesktopPlayer : MonoBehaviour
{
    public GameObject playerPrefab;

    //this is to get around some of the network behaviors being funky if they start off disabled
    public void spawnPlayer()
    {
        Instantiate(playerPrefab, transform);
    }
}
