using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(UnityEngine.XR.Interaction.Toolkit.Interactables.XRSimpleInteractable))]
public class OutlineOnHoverSimple : MonoBehaviour
{
    private UnityEngine.XR.Interaction.Toolkit.Interactables.XRSimpleInteractable interactable;
    private Outline outline;

    void Awake()
    {
        interactable = GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRSimpleInteractable>();
        outline = GetComponent<Outline>();

        if (outline != null)
            outline.enabled = false; // apagado al inicio

        // Suscribirse a eventos de hover
        interactable.hoverEntered.AddListener(OnHoverEnter);
        interactable.hoverExited.AddListener(OnHoverExit);
    }

    private void OnHoverEnter(HoverEnterEventArgs args)
    {
        if (outline != null)
            outline.enabled = true;
    }

    private void OnHoverExit(HoverExitEventArgs args)
    {
        if (outline != null)
            outline.enabled = false;
    }
}
