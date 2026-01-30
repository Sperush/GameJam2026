using UnityEngine;
using UnityEngine.UI;

public class SettingsPanel : MonoBehaviour
{
    [Header("UI")]
    public Slider sizeSlider;
    public Image colorPreview;
    public Button closeButton;

    private DummyStats target;

    public void BindTarget(GameObject targetObject)
    {
        target = targetObject.GetComponent<DummyStats>();
        if (target == null)
        {
            Debug.LogWarning("Target has no DummyStats");
            return;
        }
        sizeSlider.value = target.size;
        colorPreview.color = target.color;
        sizeSlider.onValueChanged.AddListener(OnSizeChanged);
        closeButton.onClick.AddListener(Close);
    }

    void OnSizeChanged(float value)
    {
        target.size = value;
        target.transform.localScale = Vector3.one * value;
    }

    public void SetColor(Color color)
    {
        target.color = color;
        target.GetComponent<SpriteRenderer>().color = color;
        colorPreview.color = color;
    }

    void Close()
    {
        Destroy(gameObject);
    }
}
