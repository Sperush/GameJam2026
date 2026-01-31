using UnityEngine;
using System.Collections.Generic;

public class LabelReceiver : MonoBehaviour
{
    public ButtonType typeBtn;
    public Transform lable;      // label đang gắn
    public Canvas canvas;        // canvas gốc

    public bool ApplyLabel(Transform newLabel)
    {
        DragLable newDrag = newLabel.GetComponent<DragLable>();
        if (newDrag == null) return false;
        // Nếu đã có label
        if (lable != null)
        {
            DragLable oldDrag = lable.GetComponent<DragLable>();

            // Nếu cùng slot → thay thế
            if (oldDrag.type != newDrag.type)
            {
                // đẩy label cũ ra canvas
                lable.SetParent(canvas.transform);
                lable.localPosition = Vector3.zero;
            }
            else
            {
                LabelReceiver receiver = oldDrag.GetComponentInParent<LabelReceiver>();
                if (receiver != null)
                {
                    receiver.lable = null;
                }
                lable.SetParent(transform.parent);
                lable.localPosition = Vector3.zero;
                return false;
            }
        }

        // Gắn label mới vào object
        newLabel.SetParent(transform);
        newLabel.localPosition = Vector3.zero;
        lable = newLabel;
        return true;
    }
}
