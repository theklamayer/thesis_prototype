using Unity.Netcode;
using UnityEngine;

public class ShowObjectOnHost : NetworkBehaviour
{
    [SerializeField] private GameObject targetObject;

    // Wird auf dem Client aufgerufen, sendet Befehl an Host
    public void RequestShowOnHost()
    {
        ShowOnHostServerRpc();
    }

    // Läuft nur auf dem Server (= Host)
    [ServerRpc(RequireOwnership = false)]
    private void ShowOnHostServerRpc()
    {
        if (IsHost && targetObject != null)
        {
            targetObject.SetActive(true);
        }
    }
}
