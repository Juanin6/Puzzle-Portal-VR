using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Collider))]
public class XRPortalSceneChangeConf : MonoBehaviour
{
    [Header("Nombre de la escena a cargar")]
    public string sceneToLoad;
    
    private Portal_Controller portalController;

    private void Start()
    {
        portalController = GetComponent<Portal_Controller>();
    }

    private void OnTriggerEnter(Collider other)
    {
        // Solo deja pasar al "Player"
        if (!other.CompareTag("Player")) return;

        // Solo si el portal ya está activado
        if (portalController != null && portalController.IsActivated())
        {
            
            SceneManager.LoadScene(sceneToLoad, LoadSceneMode.Single);
        }
        else
        {
            Debug.Log("El portal aún no está activado");
        }
    }
}
