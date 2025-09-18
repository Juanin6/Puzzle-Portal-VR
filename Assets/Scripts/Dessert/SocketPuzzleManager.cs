using System.Collections;
using UnityEngine;


public class SocketPuzzleManager : MonoBehaviour
{
    [Header("Sockets a completar")]
    public UnityEngine.XR.Interaction.Toolkit.Interactors.XRSocketInteractor[] sockets;  // Asigna los sockets
    public Rift_Controller portal;        // Script del portal
    public Transform portalTransform;     // Transform del portal
    public Vector3 appearOffset = new Vector3(0, 3f, 0); // cuánto se mueve hacia arriba
    public float moveSpeed = 2f;

    [Header("Feedback de puzzle")]
    public Light[] lightsToActivate;       // Luces que se prenden al completar
    public AudioSource puzzleCompleteSound; // Sonido de confirmación

    private bool puzzleCompleted = false;
    private Vector3 portalStartPos;
    private Vector3 portalTargetPos;

    private void Start()
    {
        if (portalTransform != null)
        {
            // Guardamos su posición final (donde debe quedar)
            portalTargetPos = portalTransform.position;
            // Lo mandamos bajo tierra al inicio
            portalStartPos = portalTargetPos - appearOffset;
            portalTransform.position = portalStartPos;
        }

        // Apagamos luces al inicio
        if (lightsToActivate != null)
        {
            foreach (var l in lightsToActivate)
            {
                if (l != null) l.enabled = false;
            }
        }
    }

    private void Update()
    {
        if (puzzleCompleted) return;

        bool allFilled = true;
        foreach (var socket in sockets)
        {
            if (socket == null || !socket.hasSelection)
            {
                allFilled = false;
                break;
            }
        }

        if (allFilled)
        {
            puzzleCompleted = true;
            Debug.Log("Puzzle completado: activando portal.");

            // Activa el portal
            if (portal != null)
                portal.F_ToggleRift(true);

            // Efecto de luces
            if (lightsToActivate != null)
            {
                foreach (var l in lightsToActivate)
                {
                    if (l != null) l.enabled = true;
                }
            }

            // Sonido de confirmación
            if (puzzleCompleteSound != null)
                puzzleCompleteSound.Play();

            // Animación de subida
            if (portalTransform != null)
                StartCoroutine(MovePortalUp());
        }
    }

    private IEnumerator MovePortalUp()
    {
        while (Vector3.Distance(portalTransform.position, portalTargetPos) > 0.01f)
        {
            portalTransform.position = Vector3.Lerp(
                portalTransform.position,
                portalTargetPos,
                Time.deltaTime * moveSpeed
            );
            yield return null;
        }

        portalTransform.position = portalTargetPos;
    }
}
