using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SpringBackSlider : MonoBehaviour
{
    public Slider slider;
    public float springSpeed = 12f;
    public float threshold = 0.1f;

    private float lockedValue;
    private bool isSpringing;

    void Start()
    {
        lockedValue = slider.maxValue; // giá trị bị khóa
        slider.value = lockedValue;

        slider.onValueChanged.AddListener(OnValueChanged);
    }

    void OnValueChanged(float value)
    {
        GameController.Instance.txtSalary.SetText("Salary: " + (int)slider.value + " Gold");
        GameController.Instance.typewriter.isMove = slider.value == 0f;
        if (isSpringing) return;

        // nếu player kéo xuống
        if (GameController.Instance.isBlockSlider && value < lockedValue)
        {
            StartSpringBack();
        }
    }

    void StartSpringBack()
    {
        if (isSpringing) return;

        isSpringing = true;
        StartCoroutine(SpringBack());
    }

    IEnumerator SpringBack()
    {
        while (Mathf.Abs(slider.value - lockedValue) > threshold)
        {
            slider.value = Mathf.Lerp(
                slider.value,
                lockedValue,
                Time.deltaTime * springSpeed
            );
            yield return null;
        }

        slider.value = lockedValue;
        isSpringing = false;
    }
}
