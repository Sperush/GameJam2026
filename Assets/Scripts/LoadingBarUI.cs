using System.Collections;
using UnityEngine;

public class LoadingBarUI : MonoBehaviour
{
    public RectTransform rect;
    public float speed = 200f;
    public float maxW = 596.78f;
    public bool blocked = true;
    public bool isChat;
    private bool isRung;
    bool IsOutOfScreen(RectTransform rt)
    {
        Vector3[] corners = new Vector3[4];
        rt.GetWorldCorners(corners);

        // góc phải trên
        Vector3 topRight =
            RectTransformUtility.WorldToScreenPoint(
                Camera.main,
                corners[2]
            );

        return topRight.x > Screen.width;
    }

    void Update()
    {
        if (rect.rect.width < maxW)
        {
            rect.sizeDelta += Vector2.right * speed * Time.deltaTime;

            Rect r = rect.rect;
            int sz = (int)(r.width / maxW * 100);
            if (maxW < 900) Level1UIManager.Instance.txtblock99.SetText(Mathf.Max(sz - 1, 0) + "%");
            if ((isChat && r.width >= maxW && !isRung) || (!blocked && !isRung))
            {
                Level1UIManager.Instance.txtblock99.SetText("100%");
                //    GameController.Instance.Play(VFXType.hit);
                //    GameController.Instance.cameraShake.Shake(1f, 1f);
                //    StartCoroutine(Rung());
                //    isRung = true;
            }
        }
        else if (!isChat)
        {
            isChat = true;
            StartCoroutine(Level1UIManager.Instance.Sequence());
        }
        if (!isRung && IsOutOfScreen(rect))
        {
            isRung = true;
            PanelManager.Instance.Play(VFXType.hit);
            Level1UIManager.Instance.cameraShake.Shake(1f, 10f);
            StartCoroutine(Rung());
        }
    }

    public IEnumerator Rung()
    {
        yield return new WaitForSeconds(3f);
        GameController.Instance.NextScene();

    }

    public void Boost()
    {
        maxW = 929.3f;
        speed *= 5f;
    }
}
