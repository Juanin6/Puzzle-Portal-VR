using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class LimitesDeEscena : MonoBehaviour
{
    // Referencia al Collider que delimita la escena
    public Collider limiteCollider;
    private XROrigin rig;

    private void Start()
    {
        // Verificar si se ha asignado un Collider
        if (limiteCollider == null)
        {
            Debug.LogError("No se ha asignado un Collider para los límites de la escena.");
        }

        // Obtener referencia al componente XRRig
        rig = FindObjectOfType<XROrigin>();
    }

    private void Update()
    {
        // Verificar si el jugador está fuera de los límites
        if (!limiteCollider.bounds.Contains(rig.Camera.transform.position))
        {
            // Si el jugador está fuera de los límites, detener su movimiento
            rig.MoveCameraToWorldLocation(new Vector3(-1.5999999f, 0.545000076f, 10.3100004f));

            // Aquí puedes agregar cualquier otra lógica que desees para manejar el hecho de que el jugador está fuera de los límites
        }
    }
}
