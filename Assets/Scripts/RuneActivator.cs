using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class RuneTouchActivator : MonoBehaviour
{
    [Header("Feedback Visual")]
    public Light glow;                        // Luz que se encenderá
    public ParticleSystem activateParticles;  // Partículas al activar
    public Color emissionColor = Color.green; // Color de emisión
    public float emissionIntensity = 2f;      // Intensidad de emisión

    private bool activated = false;
    private PortalPuzzleManager puzzleManager;

    private void Start()
    {
        puzzleManager = FindObjectOfType<PortalPuzzleManager>();

        // Apagar feedback al inicio
        if (glow) glow.enabled = false;
        if (activateParticles) activateParticles.Stop();
    }

    // Se activa cuando un Interactor (mano o rayo XR) lo selecciona
    public void OnSelectEnter(SelectEnterEventArgs args)
    {
        if (activated) return;

        activated = true;
        Debug.Log("Runa activada por interacción");

        // Notificar al puzzle manager
        if (puzzleManager) puzzleManager.RuneActivated();

        // Encender emisión en el material
        var rend = GetComponent<Renderer>();
        if (rend)
        {
            var mat = rend.material;
            mat.EnableKeyword("_EMISSION");
            mat.SetColor("_EmissionColor", emissionColor * emissionIntensity);
        }

        // Encender luz
        if (glow) glow.enabled = true;

        // Lanzar partículas
        if (activateParticles) activateParticles.Play();
    }
}
