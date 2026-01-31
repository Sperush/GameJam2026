using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level2Controller : MonoBehaviour
{
    public GameObject obj;
    public Animator objHoDen;
    public GameObject pointHut;
    public GameObject[] objPLs;
    public void Awake()
    {
        TranferObj();
    }
    public void TranferObj()
    {
        Vector3 center = pointHut.transform.position;

        obj.transform.DOMoveX(obj.transform.position.x + 8f, 1f).SetEase(Ease.Linear).OnComplete(() =>
        {
            Level1UIManager.Instance.cameraShake.Shake(1f, 10f);
            objHoDen.SetBool("isRun", true);
            Sequence seq = DOTween.Sequence();
            foreach (var m in objPLs)
            {
                seq.Join(DOTween.Sequence().AppendCallback(() =>
                    {
                        VortexMove(m.transform, center, 1.2f);
                    })
                );
            }
            seq.OnComplete(() =>
            {
            });
        });
    }

    void VortexMove(Transform target, Vector3 center, float duration)
    {
        float angle = Random.Range(0f, 360f);
        float radius = Vector3.Distance(target.position, center);

        DOTween.To(() => 0f, t =>
        {
            float a = angle + t * 720f; // xoay 2 vòng
            float r = Mathf.Lerp(radius, 0f, t);
            Vector3 offset = new Vector3(Mathf.Cos(a * Mathf.Deg2Rad), Mathf.Sin(a * Mathf.Deg2Rad), 0) * r;
            target.position = center + offset;
        }, 1f, duration ).SetEase(Ease.InQuad).OnComplete(()=> {
            foreach (var m in objPLs) m.SetActive(false);
        });
    }
}
