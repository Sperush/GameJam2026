using UnityEngine;

public class LabelReceiver : MonoBehaviour
{
    private LabelBehaviour currentBehaviour;

    public Transform labelSpawnPoint; // nơi label bật ra

    public void ApplyLabel(LabelData newData)
    {
        // 🔁 nếu đã có label → đẩy label cũ ra
        if (currentBehaviour != null)
        {
            EjectCurrentLabel();
        }

        // ➕ gắn label mới
        GameObject behaviourObj =
            Instantiate(newData.behaviourPrefab, transform);

        currentBehaviour = behaviourObj.GetComponent<LabelBehaviour>();
        currentBehaviour.OnApply();
    }

    void EjectCurrentLabel()
    {
        LabelData oldData = currentBehaviour.labelData;

        currentBehaviour.OnRemove();
        Destroy(currentBehaviour.gameObject);

        // 🏷️ spawn lại label UI để player kéo tiếp
        if (oldData.labelUIPrefab != null)
        {
            Vector3 spawnPos =
                labelSpawnPoint != null
                ? labelSpawnPoint.position
                : transform.position + Vector3.right * 1.2f;

            Instantiate(oldData.labelUIPrefab, spawnPos, Quaternion.identity);
        }

        currentBehaviour = null;
    }
}
