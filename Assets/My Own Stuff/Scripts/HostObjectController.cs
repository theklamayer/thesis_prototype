using Unity.Netcode;
using UnityEngine;

public class HostObjectController : NetworkBehaviour
{
    [SerializeField] private GameObject hostOnlyObject;

    public override void OnNetworkSpawn()
    {
        if (hostOnlyObject != null)
        {
            hostOnlyObject.SetActive(IsHost);
        }
    }
}

