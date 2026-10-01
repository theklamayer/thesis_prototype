using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ShowLabelOnHoverOrSelect : MonoBehaviour
{
    [SerializeField] private GameObject labelCanvas;

    private UnityEngine.XR.Interaction.Toolkit.Interactables.XRBaseInteractable interactable;

    private bool isHovered = false;
    private bool isSelected = false;

    void Awake()
    {
        interactable = GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRBaseInteractable>();

        if (labelCanvas != null)
        {
            labelCanvas.SetActive(false);
        }

        interactable.hoverEntered.AddListener(OnHoverEntered);
        interactable.hoverExited.AddListener(OnHoverExited);
        interactable.selectEntered.AddListener(OnSelectEntered);
        interactable.selectExited.AddListener(OnSelectExited);
    }

    private void OnHoverEntered(HoverEnterEventArgs args)
    {
        isHovered = true;
        UpdateLabelVisibility();
    }

    private void OnHoverExited(HoverExitEventArgs args)
    {
        isHovered = false;
        UpdateLabelVisibility();
    }

    private void OnSelectEntered(SelectEnterEventArgs args)
    {
        isSelected = true;
        UpdateLabelVisibility();
    }

    private void OnSelectExited(SelectExitEventArgs args)
    {
        isSelected = false;
        UpdateLabelVisibility();
    }

    private void UpdateLabelVisibility()
    {
        if (labelCanvas != null)
        {
            labelCanvas.SetActive(isHovered || isSelected);
        }
    }
}

