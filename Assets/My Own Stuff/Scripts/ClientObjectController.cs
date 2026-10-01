using Unity.Netcode;
using UnityEngine;

public class ClientObjectController : NetworkBehaviour
{
    [SerializeField] private GameObject clientOnlyObject;

    public override void OnNetworkSpawn()
    {
        if (clientOnlyObject != null)
        {
            clientOnlyObject.SetActive(IsClient && !IsHost); // Nur Clients, nicht der Host
        }
    }
}

