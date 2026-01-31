using UnityEngine;
using System.Collections.Generic;

public class LabelReceiver : MonoBehaviour
{
    public ButtonType typeBtn;
    public Transform lable;      // label đang gắn

    public bool ApplyLabel(Transform newLabel)
    {
        DragLable newDrag = newLabel.GetComponent<DragLable>();
        if (newDrag == null) return false;

        // 🔒 TH3: kéo vào chính mình → snapback
        if (newLabel.parent == transform)
        {
            newLabel.localPosition = Vector3.zero;
            return false;
        }

        // 🔄 nếu object đã có label
        if (lable != null)
        {
            DragLable oldDrag = lable.GetComponent<DragLable>();

            // 🔁 TH2: thay thế label
            if (oldDrag != null)
            {
                // tìm slot trống cho label cũ
                bool moved = false;
                foreach (var m in GameController.Instance.indexLable)
                {
                    if (m.typeBtn != typeBtn && m.lable == null)
                    {
                        lable.SetParent(m.transform);
                        lable.localPosition = Vector3.zero;
                        m.lable = lable;
                        moved = true;
                        break;
                    }
                }

                // không có slot trống → không cho thả
                if (!moved)
                {
                    newLabel.localPosition = Vector3.zero;
                    return false;
                }
            }
        }

        newLabel.SetParent(transform);
        newLabel.localPosition = Vector3.zero;
        lable = newLabel;

        return true;
    }
}
