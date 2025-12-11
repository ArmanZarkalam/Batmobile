using UnityEngine;
using UnityEngine.Rendering.Universal;
using System.Collections;

public class AlertEffects : MonoBehaviour
{
    public Light2D lightA;
    public Light2D lightB;
    public AudioSource alarmSource;
    public float blinkInterval = 0.3f;

    Coroutine blinkRoutine;

    public void SetAlert(bool active)
    {
        if (active)
        {
            if (blinkRoutine == null) blinkRoutine = StartCoroutine(Blink());
            if (alarmSource != null && !alarmSource.isPlaying) alarmSource.Play();
        }
        else
        {
            if (blinkRoutine != null)
            {
                StopCoroutine(blinkRoutine);
                blinkRoutine = null;
            }
            SetLights(false, false);
            if (alarmSource != null) alarmSource.Stop();
        }
    }

    IEnumerator Blink()
    {
        while (true)
        {
            SetLights(true, false);
            yield return new WaitForSeconds(blinkInterval);
            SetLights(false, true);
            yield return new WaitForSeconds(blinkInterval);
        }
    }

    void SetLights(bool a, bool b)
    {
        if (lightA != null) lightA.enabled = a;
        if (lightB != null) lightB.enabled = b;
    }
}