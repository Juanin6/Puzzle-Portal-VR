using UnityEngine;

public class PortalPuzzleManager : MonoBehaviour
{
    [Header("Portal a activar")]
    public Portal_Controller portalController; // referencia al script Portal_Controller
    public int totalRunes = 4;
    private int activatedRunes = 0;

    public void RuneActivated()
    {
        activatedRunes++;
        Debug.Log("Runa activada! Total: " + activatedRunes);

        if (activatedRunes >= totalRunes)
        {
            ActivatePortal();
        }
    }

    private void ActivatePortal()
    {
        if (portalController != null)
        {
            portalController.TogglePortal(true); // << aquí usas tu animación/efectos
            Debug.Log("¡Todas las runas activadas! El portal está abierto.");
        }
        else
        {
            Debug.LogWarning("No hay Portal_Controller asignado en el PuzzleManager.");
        }
    }
}
