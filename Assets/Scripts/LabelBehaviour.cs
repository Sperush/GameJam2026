using UnityEngine;

public abstract class LabelBehaviour : MonoBehaviour
{
    public LabelData labelData;
    public abstract void OnApply();
    public abstract void OnRemove();
}