using Unity.Netcode;
using UnityEngine;

public class VisibilityController : NetworkBehaviour
{
    [SerializeField] private GameObject targetObject;

    public void OnClientButtonClicked()
    {
        if (IsClient)
        {
            ShowObjectForAllServerRpc();
        }
    }

    [ServerRpc(RequireOwnership = false)]
    private void ShowObjectForAllServerRpc()
    {
        ShowObjectForAllClientsClientRpc();
    }

    [ClientRpc]
    private void ShowObjectForAllClientsClientRpc()
    {
        targetObject.SetActive(true);
    }
}
