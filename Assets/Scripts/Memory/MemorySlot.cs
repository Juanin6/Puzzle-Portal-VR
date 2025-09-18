using System;
using System.Collections;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class MemorySlot : MonoBehaviour
{
    [Header("Feedback")]
    public Light pcLight;                 // Luz de la PC
    public Color activeColor = Color.green;
    public AudioSource insertSound;       // Sonido al insertar

    [Header("Animación de inserción")]
    public float smoothSpeed = 5f;
    public float preOffset = 0.15f;       // Distancia previa antes de entrar
    public float approachDuration = 0.2f; // Tiempo para acercarse
    public float insertDuration = 0.3f;   // Tiempo para insertarse

    private UnityEngine.XR.Interaction.Toolkit.Interactors.XRSocketInteractor socket;

    // --- Nuevo ---
    public bool IsFilled { get; private set; } = false;
    public event Action OnCardInserted;

    private void Awake()
    {
        socket = GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactors.XRSocketInteractor>();
        socket.selectEntered.AddListener(OnMemoryInserted);
    }

    private void OnDestroy()
    {
        socket.selectEntered.RemoveListener(OnMemoryInserted);
    }

    private void OnMemoryInserted(SelectEnterEventArgs args)
    {
        if (IsFilled) return; // evita doble inserción

        Debug.Log("Memoria insertada en la PC!");

        // Marcar como lleno
        IsFilled = true;
        OnCardInserted?.Invoke(); // Notificar al Manager

        // Feedback visual y sonoro
        if (pcLight != null)
            pcLight.color = activeColor;

        if (insertSound != null)
            insertSound.Play();

        // Animación de inserción
        Transform obj = args.interactableObject.transform;
        Transform attach = socket.attachTransform;
        StartCoroutine(AnimateInsert(obj, attach));
    }

    private IEnumerator AnimateInsert(Transform obj, Transform attach)
    {
        // --- Fase 1: acercarse desde el frente ---
        Vector3 prePos = attach.position - attach.forward * preOffset;
        Quaternion preRot = attach.rotation;

        Vector3 startPos = obj.position;
        Quaternion startRot = obj.rotation;

        float elapsed = 0f;
        while (elapsed < approachDuration)
        {
            float t = elapsed / approachDuration;
            obj.position = Vector3.Lerp(startPos, prePos, t);
            obj.rotation = Quaternion.Slerp(startRot, preRot, t);

            elapsed += Time.deltaTime;
            yield return null;
        }

        obj.position = prePos;
        obj.rotation = preRot;

        // --- Fase 2: insertar hacia dentro ---
        elapsed = 0f;
        Vector3 finalPos = attach.position;
        Quaternion finalRot = attach.rotation;

        while (elapsed < insertDuration)
        {
            float t = elapsed / insertDuration;
            obj.position = Vector3.Lerp(prePos, finalPos, t);
            obj.rotation = Quaternion.Slerp(preRot, finalRot, t);

            elapsed += Time.deltaTime;
            yield return null;
        }

        obj.position = finalPos;
        obj.rotation = finalRot;
    }
}
