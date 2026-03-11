using Unity.Netcode;
using UnityEngine;

public class Spawn : NetworkBehaviour
{
    public GameObject desktopPrefab;
    public GameObject vrPrefab;

    public void CallSpawnDesktop()
    {
        SpawnDesktopServerRpc();
    }

    [ServerRpc(RequireOwnership = false)]
    void SpawnDesktopServerRpc(ServerRpcParams rpcParams = default)
    {
        ulong clientId = rpcParams.Receive.SenderClientId;

        GameObject player = Instantiate(desktopPrefab);
        player.GetComponent<NetworkObject>().SpawnAsPlayerObject(clientId);
    }
}