using Unity.Netcode;
using UnityEngine;

public class SpawnButton : MonoBehaviour
{
    Spawn spawnManager;
    private void Awake()
    {
       spawnManager = FindAnyObjectByType<Spawn>();
    }

    public void CallSpawnDesktop()
    {
        spawnManager.CallSpawnDesktop();
    }
}
