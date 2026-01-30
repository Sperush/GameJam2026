using UnityEngine;

public class QuitBehaviour : LabelBehaviour
{
    public override void OnApply()
    {
        Debug.Log($"[QUIT] {gameObject.name} is removed");
        Destroy(gameObject);
    }

    public override void OnRemove()
    {
        // QUIT thường không cần remove
    }
}