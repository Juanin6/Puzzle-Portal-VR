using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable))]
public class OutlineOnHover : MonoBehaviour
{
    private UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable grab;
    private Outline outline;

    void Awake()
    {
        grab = GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable>();
        outline = GetComponent<Outline>();

        if (outline != null)
            outline.enabled = false; // apagado al inicio

        // Suscribirse a eventos
        grab.hoverEntered.AddListener(OnHoverEnter);
        grab.hoverExited.AddListener(OnHoverExit);
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
