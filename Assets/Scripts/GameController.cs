using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public int level = 0;
    public Transform Canvas;
    public static GameController Instance;
    public TypewriterTMP typewriter;
    public TMP_Text txtSalary;
    public Image imgLock;
    public bool isBlock = true;
    public bool isBlockSlider = true;
    public GameObject Box;
    public GameObject labelStart;
    public GameObject Char;
    public void Awake()
    {
        Instance = this;
    }
    public void Collect()
    {
        var m = Box.GetComponentInChildren<DragLable>();
        if (isBlock || m == null || m.type != LabelType.Quit) return;
        labelStart.SetActive(true);
    }
    public void NextScene()
    {
        SceneManager.LoadScene("Level"+ (level+1));
    }
}
