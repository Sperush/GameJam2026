using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro; // Tận dụng DOTween bạn vừa hỏi

public class LoadingController : MonoBehaviour
{
    [Header("Cài đặt UI")]
    public Slider loadingBar;

    void Start()
    {
        // 2. Bắt đầu load scene
        StartCoroutine(LoadSceneAsync());
    }
    IEnumerator LoadSceneAsync()
    {
        // Bắt đầu load ngầm scene gameplay
        AsyncOperation operation = SceneManager.LoadSceneAsync("Level0");

        // Ngăn không cho chuyển scene ngay lập tức (để chờ loading bar chạy hết)
        operation.allowSceneActivation = false;

        float progress = 0f;

        while (progress <= 1f)
        { 
            // Ta cộng thêm thời gian để bar chạy mượt (giả vờ load)
            progress += Time.deltaTime * 0.2f; // Tốc độ chạy thanh loading

            // Update thanh Slider
            loadingBar.value = progress;

            // Nếu thanh đã đầy (>= 1) và Scene thực tế đã load xong (0.9)
            if (progress >= 1f && operation.progress >= 0.9f)
            {
                // Cho phép chuyển cảnh
                operation.allowSceneActivation = true;
                BackgroundMusic.Instance.ChangeMusic(1);
            }

            yield return null;
        }
    }
}