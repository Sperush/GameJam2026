using UnityEngine;
using System.Collections;

public class CameraShakeSmooth : MonoBehaviour
{
    Vector3 origin;

    void Awake()
    {
        origin = transform.localPosition;
    }

    public void Shake(float duration, float amplitude, float frequency = 25f)
    {
        StopAllCoroutines();
        StartCoroutine(ShakeRoutine(duration, amplitude, frequency));
    }

    IEnumerator ShakeRoutine(float d, float a, float f)
    {
        float t = 0f;

        while (t < d)
        {
            float x = Mathf.Sin(Time.time * f) * a;
            float y = Mathf.Cos(Time.time * f) * a;

            transform.localPosition = origin + new Vector3(x, y, 0);
            t += Time.deltaTime;
            yield return null;
        }

        transform.localPosition = origin;
    }
}
