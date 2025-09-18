using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Collider))]
public class PortalSceneLoader : MonoBehaviour
{
    [Header("Configuraci�n de escena")]
    public string sceneToLoad; // Nombre exacto de la escena en Build Settings

    [Header("Referencia al portal")]
    public PortalRound_Controller portal;

    private void OnTriggerEnter(Collider other)
    {
        if (portal != null && other.CompareTag("Player"))
        {
            // Solo carga la escena si el portal est� activo
            if (IsPortalActive())
            {
                Debug.Log("Portal activado, cambiando a la escena: " + sceneToLoad);
                SceneManager.LoadScene(sceneToLoad, LoadSceneMode.Single);
            }
            else
            {
                Debug.Log("El portal a�n no est� activo.");
            }
        }
    }

    private bool IsPortalActive()
    {
        // Aqu� revisamos si el portal est� encendido
        
        return portal.IsActive();
    }
}
