using UnityEngine;
using UnityEngine.EventSystems;
public enum LabelType
{
    Null,
    Settings,
    Quit,
    block99,
    Start
}
public enum ButtonType
{
    Null,
    Start,
    Settings,
    Quit,
    player,
    uncontract,
    unlock
}
public class DragLable : MonoBehaviour, IDragHandler, IPointerClickHandler, IBeginDragHandler, IEndDragHandler
{
    public RectTransform loadingBar;

    private Vector3 offset;
    private CanvasGroup canvasGroup;
    public LabelReceiver old;
    public LabelType type;
    void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        AudioManager.Instance.Play(GameSound.clickButtonSound);
        if (GameController.Instance != null && GameController.Instance.isDisControl) return;
        old = transform.parent.GetComponent<LabelReceiver>();
        transform.SetParent(PanelManager.Instance.Canvas);
        canvasGroup.blocksRaycasts = false;
        offset = transform.position - GetMouseWorldPos();
    }
    Vector3 GetMouseWorldPos() //lấy vị trí chuột
    {
        Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition); //chuyển từ tọa độ screen(màn hình điện thoại) sang tọa độ world(Unity)
        pos.z = 0;
        return pos;
    }
    public void OnDrag(PointerEventData eventData)
    {
        if (GameController.Instance != null && GameController.Instance.isDisControl) return;
        transform.position = eventData.position;
        transform.position = GetMouseWorldPos() + offset;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        AudioManager.Instance.Play(GameSound.snapSound);
        if (GameController.Instance != null && GameController.Instance.isDisControl)
        {
            SnapBack();
            return;
        }

        canvasGroup.blocksRaycasts = true;

        // 🟦 CASE ĐẶC BIỆT: block99
        if (type == LabelType.block99)
        {
            bool stillBlocking = UIOverlapUtil.IsOverlapping(
                transform as RectTransform,
                loadingBar,
                Camera.main
            );

            if (!stillBlocking)
            {
                Level1UIManager.Instance.IsOK();
            }
            else
            {
                SnapBack();
            }
            return;
        }

        GameObject target = eventData.pointerCurrentRaycast.gameObject;
        if (target == null)
        {
            SnapBack();
            return;
        }

        LabelReceiver receiver = target.GetComponentInParent<LabelReceiver>();
        if (receiver == null)
        {
            SnapBack();
            return;
        }

        // 🚫 chưa được mở box
        if (!GameController.Instance.canOpenBox &&
            receiver.typeBtn == ButtonType.unlock)
        {
            SnapBack();
            return;
        }

        // 🔗 THỬ APPLY LABEL
        bool applied = receiver.ApplyLabel(transform);
        if (!applied)
        {

            SnapBack();
            return;
        } else
        {
            if (old != null) old.lable = null;
        }

        // ✅ APPLY THÀNH CÔNG → xử lý hậu quả
        if (transform.parent.CompareTag("Player"))
        {
            HandlePlayerLabel(receiver);
        }
        else if (type == LabelType.Quit &&
                 receiver.typeBtn == ButtonType.uncontract)
        {
            PanelManager.Instance.Play(VFXType.fire);
            GameController.Instance.imgLock.color = Color.green;
            GameController.Instance.isBlockSlider = false;
        }
    }
    void SnapBack()
    {
        if (old != null)
        {
            transform.SetParent(old.transform);
            transform.localPosition = Vector3.zero;
            old.lable = transform;
        }
    }
    public void HandlePlayerLabel(LabelReceiver receiver)
    {
        switch (type)
        {
            case LabelType.Quit:
                if (!GameController.Instance.typewriter.isMove)
                {
                    PanelManager.Instance.chat.SetActive(true);
                    GameController.Instance.typewriter.Play(
                        "Are you crazy? I have a wife, 2 kids, and a house loan! I can't quit this job!"
                    );
                }
                else
                {
                    GameController.Instance.typewriter.moving = true;
                    PanelManager.Instance.ClosePanel();
                    PanelManager.Instance.chat.SetActive(true);
                    GameController.Instance.isDisControl = true;
                    GameController.Instance.typewriter.Play(
                        "WHAT?! Working for exposure?! I QUIT!!!"
                    );
                }
                break;

            case LabelType.Settings:
                PanelManager.Instance.OpenPanel(
                    PanelManager.Instance.charPanel
                );
                break;
        }
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        if (GameController.Instance.isDisControl) return;
        GameObject target;
        LabelReceiver receiver;
        switch (type)
        {
            case LabelType.Start:
                target = eventData.pointerCurrentRaycast.gameObject;
                if (target == null) return;
                receiver = target.GetComponentInParent<LabelReceiver>();
                if (receiver != null && receiver.typeBtn == ButtonType.Start)
                {
                    GameController.Instance.NextScene();
                }
                break;
            case LabelType.Settings:
                target = eventData.pointerCurrentRaycast.gameObject;
                if (target == null) return;
                receiver = target.GetComponentInParent<LabelReceiver>();
                if (receiver != null)
                {
                    PanelManager.Instance.OpenPanel(GetPanelOpen(receiver.typeBtn));
                }
                break;
            case LabelType.Quit:
                target = eventData.pointerCurrentRaycast.gameObject;
                if (target == null) return;
                receiver = target.GetComponentInParent<LabelReceiver>();
                if (receiver != null)
                {
                    if(receiver.typeBtn == ButtonType.Quit)
                    {
                        Application.Quit();
                    } else if(receiver.typeBtn == ButtonType.player)
                    {
                        PanelManager.Instance.chat.SetActive(true);
                        GameController.Instance.typewriter.Play("Are you crazy? I have a wife, 2 kids, and a house loan! I can't quit this job!");
                    }
                    else if(GameController.Instance.typewriter.isMove && receiver.typeBtn == ButtonType.unlock)
                    {
                        GameController.Instance.typewriter.moving = true;
                        PanelManager.Instance.ClosePanel();
                        PanelManager.Instance.chat.SetActive(true);
                        GameController.Instance.isDisControl = true;
                        GameController.Instance.typewriter.Play("WHAT?! Working for exposure?! I QUIT!!!");
                    }
                }
                break;
        }
    }
    public GameObject GetPanelOpen(ButtonType type)
    {
        switch (type)
        {
            case ButtonType.unlock: return PanelManager.Instance.boxPanel;
            case ButtonType.player: return PanelManager.Instance.charPanel;
            case ButtonType.Start:  return PanelManager.Instance.startPanel;
            case ButtonType.Settings: return PanelManager.Instance.settingPanel;
            case ButtonType.Quit: return PanelManager.Instance.quitPanel;
        }
        return null;
    }
}
