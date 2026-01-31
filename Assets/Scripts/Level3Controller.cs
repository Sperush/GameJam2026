using DG.Tweening;
using System.Collections;
using UnityEngine;

public class Level3Controller : MonoBehaviour
{
    public GameObject[] obj;
    public Transform pointToHere;
    public Transform pointToIdle;
    public void Awake()
    {
        StartCoroutine(Run());
    }
    public IEnumerator Run()
    {
        yield return new WaitForSeconds(2f);
        Animator anm = obj[0].GetComponent<Animator>();
        anm.SetBool("isWalk", true);
        obj[0].transform.DOMove(pointToHere.position, 8f).SetEase(Ease.Linear).OnComplete(() =>
        {
            anm.SetBool("isWalk", false);
            anm = obj[1].GetComponent<Animator>();
            anm.SetBool("isWalk", true);
            obj[1].transform.DOMove(pointToIdle.position, 8f).SetEase(Ease.Linear).OnComplete(() =>
            {
                anm.SetBool("isWalk", false);
            });
        });
    }
}
