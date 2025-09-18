using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class DoorInteractable : MonoBehaviour
{
    [Header("Configuración de la puerta")]
    public Transform doorTransform;   // El objeto que rota
    public float openAngle = 90f;     // Ángulo al abrir
    public float speed = 2f;          // Velocidad de rotación

    private bool isOpen = false;
    private Quaternion closedRotation;
    private Quaternion openRotation;

    private void Start()
    {
        if (doorTransform == null) doorTransform = transform;
        closedRotation = doorTransform.localRotation;
        openRotation = Quaternion.Euler(0, openAngle, 0) * closedRotation;
    }

    public void OnSelectEnter(SelectEnterEventArgs args)
    {
        Debug.Log("Puerta seleccionada -> toggle");
        ToggleDoor();
    }

    private void ToggleDoor()
    {
        isOpen = !isOpen;
        StopAllCoroutines();
        StartCoroutine(RotateDoor(isOpen ? openRotation : closedRotation));
    }

    private System.Collections.IEnumerator RotateDoor(Quaternion targetRot)
    {
        while (Quaternion.Angle(doorTransform.localRotation, targetRot) > 0.1f)
        {
            doorTransform.localRotation = Quaternion.Slerp(
                doorTransform.localRotation, targetRot, Time.deltaTime * speed
            );
            yield return null;
        }
        doorTransform.localRotation = targetRot;
    }
}
