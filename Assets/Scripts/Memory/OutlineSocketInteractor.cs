using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(UnityEngine.XR.Interaction.Toolkit.Interactors.XRSocketInteractor))]
public class OutlineSocketInteractor : MonoBehaviour
{
    private UnityEngine.XR.Interaction.Toolkit.Interactors.XRSocketInteractor socket;
    private Outline outline;

    void Awake()
    {
        socket = GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactors.XRSocketInteractor>();
        outline = GetComponent<Outline>();

        if (outline != null)
            outline.enabled = false; // apagado al inicio

        socket.hoverEntered.AddListener(OnHoverEnter);
        socket.hoverExited.AddListener(OnHoverExit);
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
