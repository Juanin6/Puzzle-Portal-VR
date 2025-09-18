using UnityEngine;

public class RunePanelPuzzle : MonoBehaviour
{
    [Header("Configuración del puzzle")]
    public int[] correctSequence = { 1, 2, 3, 4 };
    private int currentStep = 0;

    [Header("Portal a activar")]
    public PortalRound_Controller portal;

    [Header("Sonidos del puzzle")]
    public AudioSource puzzleAudio;
    public AudioClip completeSound;

    // Devuelve true si fue correcto, false si fue incorrecto
    public bool PressButton(int buttonID)
    {
        if (buttonID == correctSequence[currentStep])
        {
            currentStep++;

            if (currentStep >= correctSequence.Length)
            {
                Debug.Log("Puzzle completado. Activando portal...");
                if (portal != null)
                    portal.F_TogglePortalRound(true);

                if (puzzleAudio != null && completeSound != null)
                    puzzleAudio.PlayOneShot(completeSound);
            }

            return true; // correcto
        }
        else
        {
            Debug.Log("Secuencia incorrecta. Reiniciando puzzle...");
            currentStep = 0;
            return false; // incorrecto
        }
    }
}
