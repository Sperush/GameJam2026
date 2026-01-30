using UnityEngine;

public enum LabelType
{
    Settings,
    Quit
}

[CreateAssetMenu(menuName = "Label/Label Data")]
public class LabelData : ScriptableObject
{
    public string labelName;
    public LabelType labelType;
    public GameObject behaviourPrefab;
}

