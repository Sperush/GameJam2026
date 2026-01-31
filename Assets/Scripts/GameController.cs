using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public enum VFXType
{
    bomd,
    fire,
    hit
}
[System.Serializable]
public class VFXItem
{
    public VFXType type;
    public ParticleSystem obj;
}
public class GameController : MonoBehaviour
{
    public int level = 0;
    public Transform CanvasUI;
    public static GameController Instance;
    public TypewriterTMP typewriter;
    public TMP_Text txtSalary;
    public Image imgLock;
    public bool isBlock = true;
    public bool isBlockSlider = true;
    public GameObject Box;
    public GameObject labelStart;
    public GameObject Char;
    public bool canOpenBox;
    public bool isDisControl;
    public LabelReceiver[] indexLable;
    public void Awake()
    {
        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        } else Destroy(gameObject);
    }
    public void Collect()
    {
        var m = Box.GetComponentInChildren<DragLable>();
        if (isBlock || m == null || m.type != LabelType.Quit) return;
        labelStart.SetActive(true);
    }
    public void ChangeBox(Slider sl)
    {
        labelStart.SetActive(sl.value == 1f);
    }
    public void NextScene()
    {
        level++;
        BackgroundMusic.Instance.ChangeMusic(level == 0 ? 1:0);
        SceneManager.LoadScene("Level"+level);
    }
}
