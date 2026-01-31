using DG.Tweening;
using System.Collections;
using UnityEngine;

public class PanelManager : MonoBehaviour
{
    public static PanelManager Instance;
    public bool isOpenPanel;
    private Vector3 initialScale;
    [Header("Cài đặt")]
    public float duration = 0.5f; // Thời gian hiệu ứng
    public Ease openEase = Ease.OutBack; // Kiểu nảy khi mở
    public Ease closeEase = Ease.InBack; // Kiểu thu vào khi đóng
    public GameObject dark;
    public GameObject quitPanel;
    public GameObject settingPanel;
    public GameObject startPanel;
    public GameObject charPanel;
    public GameObject objOpen;
    public GameObject chat;
    public void Awake()
    {
        Instance = this;
    }
    public void OpenPanel(GameObject panel)
    {
        if (panel == null) return;
        dark.SetActive(true);
        if (!isOpenPanel)
        {
            objOpen = panel;
            isOpenPanel = true;
            panel.SetActive(true);
            //AudioManager.Instance.Play(GameSound.clickButtonSound);
            initialScale = new Vector3(1, 1, 1);
            panel.transform.localScale = Vector3.zero;
            panel.transform.DOScale(initialScale, duration).SetEase(openEase);
        }
        else
        {
            StartCoroutine(Open(panel));
        }
    }
    public IEnumerator Open(GameObject panel)
    {
        yield return new WaitUntil(() => isOpenPanel == false);
        objOpen = panel;
        isOpenPanel = true;
        panel.SetActive(true);
        //AudioManager.Instance.Play(GameSound.clickButtonSound);
        initialScale = new Vector3(1, 1, 1);
        panel.transform.localScale = Vector3.zero;
        panel.transform.DOScale(initialScale, duration).SetEase(openEase);
    }

    public void ClosePanel()
    {
        //AudioManager.Instance.Play(GameSound.clickButtonSound);
        // Thu nhỏ về 0
        objOpen.transform.DOScale(Vector3.zero, duration) // Đóng thì nên nhanh hơn mở 1 chút
        .SetEase(closeEase)
        .OnComplete(() =>
        {
            dark.SetActive(false);
            isOpenPanel = false;
            // Sau khi thu nhỏ xong -> Tắt toàn bộ Container (biến mất cả nền đen)
            objOpen.SetActive(false);
        });
    }
}
