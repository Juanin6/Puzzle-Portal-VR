using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class RuneButton : MonoBehaviour
{
    [Header("Config")]
    public RunePanelPuzzle puzzle;    // referencia al puzzle
    public int buttonID;              // ID único de este botón

    [Header("Sonidos")]
    public AudioSource audioSource;   // cada runa puede tener su sonido
    public AudioClip confirmSound;    // sonido cuando está correcto
    public AudioClip errorSound;      // sonido si es incorrecto

    public void OnSelectEnter(SelectEnterEventArgs args)
    {
        if (puzzle != null)
        {
            bool correcto = puzzle.PressButton(buttonID);

            if (audioSource != null)
            {
                if (correcto && confirmSound != null)
                    audioSource.PlayOneShot(confirmSound);
                else if (!correcto && errorSound != null)
                    audioSource.PlayOneShot(errorSound);
            }

            Debug.Log("Runa seleccionada: " + buttonID + (correcto ? " ✅" : " ❌"));
        }
    }
}
