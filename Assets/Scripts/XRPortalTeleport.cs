using System.Collections;
using UnityEngine;
using Unity.XR.CoreUtils;


[RequireComponent(typeof(Collider))]
public class XRPortalTeleport : MonoBehaviour
{
    [Header("Destino del portal")]
    public Transform destination;      // El ExitPoint del portal de salida
    public float exitOffset = 0.25f;   // Para evitar caer dentro del trigger otra vez
    public float cooldown = 0.25f;     // Tiempo de protecci�n anti ping-pong

    private XROrigin xrOrigin;
    private UnityEngine.XR.Interaction.Toolkit.Locomotion.Teleportation.TeleportationProvider tp;
    private bool justTeleported;

    void Awake()
    {
        xrOrigin = FindObjectOfType<XROrigin>();
        if (xrOrigin != null)
            tp = xrOrigin.GetComponent<UnityEngine.XR.Interaction.Toolkit.Locomotion.Teleportation.TeleportationProvider>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (justTeleported || tp == null || destination == null) return;

        // Verifica que lo que entra al trigger es el XR Origin
        if (other.GetComponentInParent<XROrigin>() != xrOrigin) return;

        StartCoroutine(DoTeleport());
    }

    IEnumerator DoTeleport()
    {
        var req = new UnityEngine.XR.Interaction.Toolkit.Locomotion.Teleportation.TeleportRequest
        {
            destinationPosition = destination.position + destination.forward * exitOffset,
            destinationRotation = destination.rotation,
            matchOrientation    = UnityEngine.XR.Interaction.Toolkit.Locomotion.Teleportation.MatchOrientation.TargetUpAndForward
        };

        tp.QueueTeleportRequest(req);

        justTeleported = true;
        yield return new WaitForSeconds(cooldown);
        justTeleported = false;
    }
}
