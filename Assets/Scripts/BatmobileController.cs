using UnityEngine;
using UnityEngine.Rendering.Universal;

public class BatmobileController : MonoBehaviour
{
    public enum BatState { Normal, Stealth, Alert }

    [Header("Movement")]
    public float normalSpeed = 5f;
    public float boostSpeed = 8f;
    public float stealthSpeed = 3f;
    public float rotationSpeed = 150f;

    [Header("State")]
    public BatState currentState = BatState.Normal;

    [Header("Lights & Audio")]
    public Light2D headlight;
    public AlertEffects alertEffects;

    Rigidbody2D rb;
    float moveInput;
    float turnInput;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Inputs
        moveInput = Input.GetAxisRaw("Vertical");   // W/S or Up/Down
        turnInput = -Input.GetAxisRaw("Horizontal"); // A/D or Left/Right (negative to rotate correctly)

        // State changes
        if (Input.GetKeyDown(KeyCode.C)) SetState(BatState.Stealth);
        if (Input.GetKeyDown(KeyCode.Space)) SetState(BatState.Alert);
        if (Input.GetKeyDown(KeyCode.V)) SetState(BatState.Normal); // simple way back to normal

        // Optional: toggle headlight in non-alert states
        if (Input.GetKeyDown(KeyCode.L) && currentState != BatState.Alert && headlight != null)
            headlight.enabled = !headlight.enabled;
    }

    void FixedUpdate()
    {
        float speed = GetSpeedByState();
        Vector2 forward = transform.up;
        rb.MovePosition(rb.position + forward * speed * moveInput * Time.fixedDeltaTime);
        float rot = rotationSpeed * turnInput * Time.fixedDeltaTime * Mathf.Sign(moveInput == 0 ? 1 : moveInput);
        rb.MoveRotation(rb.rotation + rot);
    }

    float GetSpeedByState()
    {
        switch (currentState)
        {
            case BatState.Stealth: return stealthSpeed;
            case BatState.Alert: return boostSpeed; // slightly faster in alert
            default:
            case BatState.Normal:
                return Input.GetKey(KeyCode.LeftShift) ? boostSpeed : normalSpeed;
        }
    }

    public void SetState(BatState newState)
    {
        currentState = newState;

        // Headlight dim/off in stealth
        if (headlight != null)
        {
            if (newState == BatState.Stealth)
            {
                headlight.intensity = 0.2f;
                headlight.enabled = true;
            }
            else
            {
                headlight.intensity = 1f;
                headlight.enabled = true;
            }
        }

        if (alertEffects != null)
            alertEffects.SetAlert(newState == BatState.Alert);
    }
}