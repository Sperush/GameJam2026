using UnityEngine;

public class LoadingBarUI : MonoBehaviour
{
    public RectTransform rect;
    public float speed = 200f;
    public float maxW = 596.78f;
    public bool blocked = false;
    private bool isChat;

    void Update()
    {
        if (!blocked)
        {
            if (rect.rect.width < maxW)
            {
                Rect r = rect.rect;
                int sz = (int)(r.width / maxW * 100);
                Level1UIManager.Instance.txtblock99.SetText(sz + "%");
                rect.sizeDelta += Vector2.right * speed * Time.deltaTime;
            }
            else if (!isChat)
            {
                isChat = true;
                StartCoroutine(Level1UIManager.Instance.Sequence());
            } else if(isChat && maxW == 729.3f)
            {
                GameController.Instance.NextScene();
            }
        }
    }

    public void Boost()
    {
        maxW = 729.3f;
        speed *= 5f;
    }
}
