using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using TMPro; // Si usas TextMeshPro

public class ScreenActivator : MonoBehaviour
{
    [Header("Pantalla / Texto")]
    public GameObject screenObject;   // El objeto de la pantalla (Canvas, Panel, Mesh, etc.)
    public TextMeshProUGUI textField; // Texto a mostrar en la pantalla
    [TextArea] public string message = "¡Bienvenido al sistema!";

    private bool isOn = false;

    // Este método se llama desde XR Simple Interactable (Select Entered)
    public void OnSelectEnter(SelectEnterEventArgs args)
    {
        ToggleScreen();
    }

    private void ToggleScreen()
    {
        isOn = !isOn;

        if (screenObject != null)
            screenObject.SetActive(isOn);

        if (isOn && textField != null)
            textField.text = message;

        Debug.Log(isOn ? "Pantalla encendida" : "Pantalla apagada");
    }
}
