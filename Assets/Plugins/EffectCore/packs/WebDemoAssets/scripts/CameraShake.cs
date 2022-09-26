using UnityEngine;
using System.Collections;

public class CameraShake : MonoBehaviour
{
    // Transform of the camera to shake. Grabs the gameObject's transform
    // if null.
    public Transform camTransform;

    // How long the object should shake for.
    static public float shakeDuration = 0f;

    // Amplitude of the shake. A larger value shakes the camera harder.
    static public float shakeAmount = 0.7f;
    public float decreaseFactor = 1.0f;

    Vector3 originalPos;

    void Awake()
    {
        if (camTransform == null)
        {
            camTransform = GetComponent(typeof(Transform)) as Transform;
        }
    }

    void OnEnable()
    {
        originalPos = camTransform.localPosition;
    }

    void Update()
    {
        if (shakeDuration > 0)
        {
            camTransform.localPosition = originalPos + Random.insideUnitSphere * shakeAmount;

            shakeDuration -= Time.deltaTime * decreaseFactor;
            
        }
        if (shakeAmount > 0)
        {
            shakeAmount -= Time.deltaTime * decreaseFactor;
        }
        else
        {
            shakeAmount = 0f;
            shakeDuration = 0f;
            camTransform.localPosition = originalPos;
        }
    }
}