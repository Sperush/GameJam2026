using UnityEngine;

public class SettingsBehaviour : LabelBehaviour
{
    public GameObject settingsPanelPrefab;
    private GameObject panelInstance;

    public override void OnApply()
    {
        panelInstance = Instantiate(settingsPanelPrefab);
        panelInstance.GetComponent<SettingsPanel>().BindTarget(gameObject);
    }

    public override void OnRemove()
    {
        if (panelInstance != null) Destroy(panelInstance);
    }
}