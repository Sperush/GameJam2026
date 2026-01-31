using UnityEngine;

public class UIOverlapCheck : MonoBehaviour
{
    public RectTransform loadingBar;
    public RectTransform block99;
    public LoadingBarUI barLogic;

    void Update()
    {
        if (RectTransformUtility.RectangleContainsScreenPoint(
            block99,
            loadingBar.position,
            Camera.main))
        {
            barLogic.blocked = true;
        }
    }
}
