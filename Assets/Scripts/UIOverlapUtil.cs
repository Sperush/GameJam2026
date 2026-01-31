using UnityEngine;

public static class UIOverlapUtil
{
    public static bool IsOverlapping(RectTransform a, RectTransform b, Camera cam)
    {
        Rect ra = GetScreenRect(a, cam);
        Rect rb = GetScreenRect(b, cam);
        return ra.Overlaps(rb);
    }

    static Rect GetScreenRect(RectTransform rt, Camera cam)
    {
        Vector3[] corners = new Vector3[4];
        rt.GetWorldCorners(corners);

        Vector2 min = RectTransformUtility.WorldToScreenPoint(cam, corners[0]);
        Vector2 max = RectTransformUtility.WorldToScreenPoint(cam, corners[2]);

        return new Rect(min, max - min);
    }
}
