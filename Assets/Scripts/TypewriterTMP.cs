using DG.Tweening;
using System.Collections;
using TMPro;
using UnityEngine;

public class TypewriterTMP : MonoBehaviour
{
    public float charDelay = 0.03f;
    public float commaDelay = 0.2f;
    public float dotDelay = 0.4f;

    private TextMeshProUGUI tmp;
    private Coroutine typingRoutine;
    private Coroutine doneChat;
    private bool isTyping;
    private string txt;
    private Tween currentHandTween;
    public bool isMove;
    public bool moving;
    void Awake()
    {
        tmp = GetComponent<TextMeshProUGUI>();
    }

    public void Play(string text)
    {
        txt = text;
        if (typingRoutine != null) StopCoroutine(typingRoutine);
        if (doneChat != null) StopCoroutine(doneChat);

        typingRoutine = StartCoroutine(TypeText(text));
    }

    IEnumerator TypeText(string text)
    {
        isTyping = true;
        tmp.text = "";

        foreach (char c in text)
        {
            tmp.text += c;

            if (c == ',' || c == ';')
                yield return new WaitForSeconds(commaDelay);
            else if (c == '.' || c == '!' || c == '?')
                yield return new WaitForSeconds(dotDelay);
            else
                yield return new WaitForSeconds(charDelay);
        }

        isTyping = false;
        doneChat = StartCoroutine(Hide());
    }
    void KillCurrentTween()
    {
        if (currentHandTween != null && currentHandTween.IsActive())
        {
            currentHandTween.Kill();
            currentHandTween = null;
        }
    }
    IEnumerator Hide()
    {
        yield return new WaitForSeconds(2f);
        if(PanelManager.Instance != null && PanelManager.Instance.chat != null) PanelManager.Instance.chat.SetActive(false);
        if (isMove && moving)
        {
            Vector3 local = GameController.Instance.Char.transform.localScale;
            local.x = -local.x;
            GameController.Instance.Char.transform.localScale = local;
            Vector3 pos2 = GameController.Instance.Char.transform.position;
            pos2.x += 10f;
            KillCurrentTween(); // Kill cái cũ nếu có

            Sequence seq = DOTween.Sequence();
            seq.Append(GameController.Instance.Char.transform.DOMove(pos2, 1f));
            currentHandTween = seq;
            seq.OnComplete(() =>
            {
                GameController.Instance.isDisControl = false;
                GameController.Instance.canOpenBox = true;
                GameController.Instance.isBlock = false;
            });
            moving = false;
            isMove = false;
        }
    }

    //public void Skip()
    //{
    //    if (!isTyping) return;

    //    StopCoroutine(typingRoutine);
    //    if(doneChat != null) StopCoroutine(doneChat);
    //    tmp.text = txt; // đảm bảo đầy đủ text
    //    isTyping = false;
    //    StartCoroutine(Hide());
    //}

    //void Update()
    //{
    //    //if (Input.GetMouseButtonDown(0))
    //    //    Skip();
    //}
}
