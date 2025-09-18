using UnityEngine;

public class KeyLauncher : MonoBehaviour
{
    [Header("Config")]
    public GameObject keyPrefab;        // Prefab de la llave
    public Transform spawnPoint;        // El punto de aparición
    public float launchForce = 500f;    // Fuerza del impulso

    private GameObject currentKey;
    private bool hasLaunched = false;   // <-- control de un solo uso

    public void LaunchKey()
    {
        // Si ya se lanzó una vez, no volver a ejecutar
        if (hasLaunched)
        {
            Debug.Log("La llave ya fue lanzada, no se puede repetir.");
            return;
        }

        // Instanciamos la llave en el SpawnPoint
        currentKey = Instantiate(keyPrefab, spawnPoint.position, spawnPoint.rotation);

        // Obtenemos el rigidbody y le aplicamos fuerza
        Rigidbody rb = currentKey.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = false; // Por si estaba congelada
            rb.AddForce(spawnPoint.forward * launchForce);
        }

        hasLaunched = true; // <-- marcamos que ya se usó
        Debug.Log("Llave lanzada!");
    }
}
