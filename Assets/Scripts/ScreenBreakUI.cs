using UnityEngine;
using UnityEngine.SceneManagement;

public class ScreenBreakUI : MonoBehaviour
{
    public RectTransform loadingBar;
    public float screenEdgeX = 900f;
    public GameObject breakFX;

    void Update()
    {
        if (loadingBar.anchoredPosition.x > screenEdgeX)
        {
            //breakFX.SetActive(true);
            Invoke(nameof(GameController.Instance.NextScene), 1f);
            enabled = false;
        }
    }
}
