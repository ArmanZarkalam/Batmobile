using UnityEngine;
using UnityEngine.Rendering.Universal;

public class BatSignalController : MonoBehaviour
{
    public Light2D signalLight;
    public SpriteRenderer signalSprite;
    public float rotationSpeed = 20f;
    bool isOn = false;
    Color offColor = new Color(0.2f, 0.2f, 0.2f, 1f); // dark gray logo when off

    void Awake()
    {
        // Start with signal off and logo dark
        SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            isOn = !isOn;
            SetActive(isOn);
        }

        if (isOn && signalLight != null)
        {
            transform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime);
        }
    }

    void SetActive(bool active)
    {
        if (signalLight != null) signalLight.enabled = active;

        if (signalSprite != null)
        {
            signalSprite.enabled = true; // always visible
            signalSprite.color = active ? Color.white : offColor;
        }
    }
}