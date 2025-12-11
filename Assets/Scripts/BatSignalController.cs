using UnityEngine;
using UnityEngine.Rendering.Universal;

public class BatSignalController : MonoBehaviour
{
    public Light2D signalLight;
    public float rotationSpeed = 20f;
    bool isOn = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B)) ToggleSignal();
        if (isOn && signalLight != null)
            transform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime);
    }

    void ToggleSignal()
    {
        isOn = !isOn;
        if (signalLight != null) signalLight.enabled = isOn;
    }
}