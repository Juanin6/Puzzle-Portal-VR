using UnityEngine;

public class OpenXRErrorHandler : MonoBehaviour
{
    private void Awake()
    {
        // Desactivar los mensajes de error
        Application.logMessageReceived += HandleLogMessage;
    }

    private void HandleLogMessage(string logString, string stackTrace, LogType type)
    {
        // Ignorar todos los mensajes de error
        if (type == LogType.Error && logString.Contains("Failure to restart OpenXRLoader after shutdown"))
        {
            Debug.unityLogger.logEnabled = false;
        }
    }

    private void OnDestroy()
    {
        // Restaurar la configuración predeterminada de los mensajes de error al destruir el objeto
        Debug.unityLogger.logEnabled = true;
    }
}
