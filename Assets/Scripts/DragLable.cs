using UnityEngine;
using UnityEngine.EventSystems;

public class DragLable : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    public LabelData labelData;
    private Canvas canvas;
    private Vector3 offset;
    void Start()
    {
        canvas = GetComponentInParent<Canvas>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        transform.SetParent(canvas.transform);
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

    public void OnPointerUp(PointerEventData eventData)
    {
        GameObject target = eventData.pointerCurrentRaycast.gameObject;
        if (target == null) return;

        LabelReceiver receiver = target.GetComponentInParent<LabelReceiver>();
        if (receiver != null)
        {
            receiver.ApplyLabel(labelData);
        }
    }
}
