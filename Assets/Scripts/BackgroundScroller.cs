using UnityEngine;

/// <summary>
/// Simple background scrolling: moves the background left/right
/// when the player presses A/D (Horizontal axis). Holding Shift
/// increases the scroll speed to feel like a racing game.
/// Attach this to your background root object (e.g. GothamBackgroundParent).
/// </summary>
public class BackgroundScroller : MonoBehaviour
{
    public float scrollSpeed = 3f;
    public float boostMultiplier = 2f;

    void Update()
    {
        // A/D or Left/Right arrows
        float horizontal = Input.GetAxisRaw("Horizontal");
        if (Mathf.Approximately(horizontal, 0f)) return;

        float speed = scrollSpeed;
        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
        {
            speed *= boostMultiplier;
        }

        // Move background opposite to input so car appears to move
        Vector3 move = Vector3.right * -horizontal * speed * Time.deltaTime;
        transform.position += move;
    }
}


