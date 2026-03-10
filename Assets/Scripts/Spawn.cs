using UnityEngine;
using UnityEngine.InputSystem;
using Unity.Netcode;

public class Spawn : NetworkBehaviour
{

    public override void OnNetworkSpawn()
    {
        if (!IsOwner)
        {
            Camera[] playerCameras = gameObject.GetComponentsInChildren<Camera>(true);
            foreach (Camera cam in playerCameras)
            {
                Destroy(cam);
            }
        }
    }


}
