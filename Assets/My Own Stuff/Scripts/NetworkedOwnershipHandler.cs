using Unity.Netcode;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class NetworkedOwnershipHandler : NetworkBehaviour
{
    private NetworkObject netObj;
    private UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable grab;

    private void Awake()
    {
        netObj = GetComponent<NetworkObject>();
        grab = GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable>();

        // XRGrabInteractable korrekt konfigurieren, um Parenting zu vermeiden:
        grab.attachTransform = new GameObject("AttachPoint").transform;
        grab.attachTransform.SetParent(transform, false); // Als Kind einfügen
        grab.attachTransform.localPosition = Vector3.zero;
        grab.movementType = UnityEngine.XR.Interaction.Toolkit.Interactables.XRBaseInteractable.MovementType.VelocityTracking;
        grab.attachEaseInTime = 0f;

        grab.selectEntered.AddListener(OnSelectEntered);
    }


    private void OnSelectEntered(SelectEnterEventArgs args)
    {
        if (!IsOwner) // Der Client darf es nicht direkt selbst machen
        {
            RequestOwnershipServerRpc(NetworkManager.Singleton.LocalClientId);
        }
    }

    [ServerRpc(RequireOwnership = false)]
    private void RequestOwnershipServerRpc(ulong clientId)
    {
        if (NetworkObject.OwnerClientId == clientId) return; // Bereits Eigentümer → nichts tun
        NetworkObject.ChangeOwnership(clientId);
    }

    [ServerRpc(RequireOwnership = false)]
    public void SetParentServerRpc(NetworkObjectReference parentRef)
    {
        if (parentRef.TryGet(out var parent))
        {
            transform.SetParent(parent.transform); // erlaubt, weil auf Server
        }
    }

}

