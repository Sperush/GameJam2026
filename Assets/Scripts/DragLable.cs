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
        old = transform.parent.GetComponent<LabelReceiver>();
        transform.SetParent(GameController.Instance.Canvas);
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
        transform.position = eventData.position;
        transform.position = GetMouseWorldPos() + offset;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = true;
        GameObject target = eventData.pointerCurrentRaycast.gameObject;
        if (type == LabelType.block99)
        {
            bool stillBlocking = UIOverlapUtil.IsOverlapping(transform as RectTransform, loadingBar, Camera.main);
            if (!stillBlocking)
            {
                Level1UIManager.Instance.IsOK();
            }
            return;
        }
        if (target == null) return;
        LabelReceiver receiver = target.GetComponentInParent<LabelReceiver>();
        if (receiver != null)
        {
            if (receiver.ApplyLabel(transform) && old != null) old.lable = null;
            if (transform.parent != null && transform.parent.CompareTag("Player"))
            {
                switch (type)
                {
                    case LabelType.Quit:
                        PanelManager.Instance.chat.SetActive(true);
                        GameController.Instance.typewriter.Play("Are you crazy? I have a wife, 2 kids, and a house loan! I can't quit this job!");
                        break;
                    case LabelType.Settings:
                        PanelManager.Instance.OpenPanel(PanelManager.Instance.charPanel);
                        break;
                }
            } else if(type == LabelType.Quit && receiver.typeBtn == ButtonType.uncontract)
            {
                GameController.Instance.imgLock.color = Color.green;
                GameController.Instance.isBlockSlider = false;
            }
        } else
        {
            if(old != null) old.lable = null;
        }
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        GameObject target;
        LabelReceiver receiver;
        switch (type)
        {
            case LabelType.Start:
                target = eventData.pointerCurrentRaycast.gameObject;
                if (target == null) return;
                receiver = target.GetComponentInParent<LabelReceiver>();
                if (receiver.typeBtn == ButtonType.Start)
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
                    else if (receiver.typeBtn == ButtonType.unlock)
                    {
                        GameController.Instance.labelStart.SetActive(true);
                    }
                }
                break;
        }
    }
    public GameObject GetPanelOpen(ButtonType type)
    {
        switch (type)
        {
            case ButtonType.player: return PanelManager.Instance.charPanel;
            case ButtonType.Start:  return PanelManager.Instance.startPanel;
            case ButtonType.Settings: return PanelManager.Instance.settingPanel;
            case ButtonType.Quit: return PanelManager.Instance.quitPanel;
        }
        return null;
    }
}
