using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(UnityEngine.XR.Interaction.Toolkit.Interactors.XRSocketInteractor))]
public class AutoSocketSnap : MonoBehaviour
{
    private UnityEngine.XR.Interaction.Toolkit.Interactors.XRSocketInteractor socket;
    private XRInteractionManager manager;

    private void Awake()
    {
        socket = GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactors.XRSocketInteractor>();
        manager = socket.interactionManager;
    }

    private void OnEnable()
    {
        socket.hoverEntered.AddListener(OnHoverEnter);
    }

    private void OnDisable()
    {
        socket.hoverEntered.RemoveListener(OnHoverEnter);
    }

    private void OnHoverEnter(HoverEnterEventArgs args)
    {
        var interactable = args.interactableObject as UnityEngine.XR.Interaction.Toolkit.Interactables.IXRSelectInteractable;
        if (interactable == null) return;

        // Si est� agarrado por otra mano/interactor, transfi�relo
        if (interactable.isSelected)
        {
            var selecting = interactable.firstInteractorSelecting;
            if (selecting != null)
                manager.SelectExit(selecting, interactable);
        }

        // Seleccionar este socket (lo hace encajar al Attach_Socket)
        if (!socket.hasSelection)
            manager.SelectEnter(socket, interactable);
    }
}
