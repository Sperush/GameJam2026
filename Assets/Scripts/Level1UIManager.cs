using UnityEngine;
using TMPro;
using System.Collections;

public class Level1UIManager : MonoBehaviour
{
    public LoadingBarUI loadingBar;
    public RectTransform block99;
    public TMP_Text txtblock99;
    public GameObject arrowHint;
    public TypewriterTMP narrator;
    public static Level1UIManager Instance;
    public CameraShakeSmooth cameraShake;

    void Start()
    {
        Instance = this;
    }

    public IEnumerator Sequence()
    {
        yield return new WaitForSeconds(4f);
        narrator.Play("Ahem. It's stuck.");
        yield return new WaitForSeconds(4f);

        narrator.Play("Hello? The number is blocking the bar. It's basic physics, genius.");
        yield return new WaitForSeconds(10f);

        arrowHint.SetActive(true);
    }

    public void IsOK()
    {
        arrowHint.SetActive(false);
        loadingBar.blocked = false;
        loadingBar.Boost();
    }
}
