using Unity.Netcode;
using UnityEngine;

public class ToggleCanvasOnClick : NetworkBehaviour
{
    [SerializeField] private GameObject targetCanvas;

    public void ToggleCanvas()
    {
        
        if (IsHost) return;     // Nur der Client darf den Canvas toggeln

        if (targetCanvas != null)
        {
            bool isActive = targetCanvas.activeSelf;
            targetCanvas.SetActive(!isActive);
        }
    }
}

