using UnityEngine;

public class MemoryPuzzleManager : MonoBehaviour
{
    public MemorySlot[] slots;   // Asigna los slots en el inspector
    public Animator doorAnimator; // Animator de la puerta
    private bool doorOpened = false;

    private void Start()
    {
        // Suscribir a cada slot
        foreach (var slot in slots)
        {
            slot.OnCardInserted += CheckAllSlots;
        }
    }

    private void CheckAllSlots()
    {
        if (doorOpened) return;

        // Verifica si todos los slots tienen memoria
        foreach (var slot in slots)
        {
            if (!slot.IsFilled)
                return;
        }

        // Si todos están llenos -> abrir puerta
        doorOpened = true;
        doorAnimator.SetTrigger("Open");
        Debug.Log("Todas las memorias insertadas, ¡puerta abierta!");
    }
}
