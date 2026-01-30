using UnityEngine;

public class LabelReceiver : MonoBehaviour
{
    private LabelBehaviour currentBehaviour;

    public void ApplyLabel(LabelData data)
    {
        if (currentBehaviour != null)
        {
            currentBehaviour.OnRemove();
            Destroy(currentBehaviour);
        }
        GameObject behaviourObj = Instantiate(data.behaviourPrefab, transform);
        currentBehaviour = behaviourObj.GetComponent<LabelBehaviour>();
        currentBehaviour.OnApply();
    }

    public void RemoveLabel()
    {
        if (currentBehaviour == null) return;
        currentBehaviour.OnRemove();
        Destroy(currentBehaviour);
        currentBehaviour = null;
    }
}
