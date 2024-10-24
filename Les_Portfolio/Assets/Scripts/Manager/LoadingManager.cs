using System.Collections;
using System.Collections.Generic;
using UISystem;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class LoadingManager : SingletonMonoBehaviour<LoadingManager>
{
    [SerializeField] GameObject fadeCanvas;
    [SerializeField] Image fadeImage;
    [SerializeField] float fadeTime = 1f;

    private Color fadeInColor = Color.clear;
    private Color fadeOutColor = Color.black;
    private bool isFade = false;

    protected override void OnAwakeSingleton()
    {
        base.OnAwakeSingleton();
        DontDestroyOnLoad(this);
    }

    #region Fade
    public void SetFadeIn()
    {
        fadeImage.DOColor(fadeInColor, fadeTime).onComplete =
        () =>
        {
            isFade = false;
            fadeCanvas.SetActive(false);
        };
    }
    public void SetFadeOut()
    {
        fadeCanvas.SetActive(true);
        fadeImage.DOColor(fadeOutColor, fadeTime).onComplete = () => isFade = true;
    }
    #endregion

    #region SceneLoad
    public void SceneLoad(string sceneName)
    {
        StartCoroutine(SceneLoadCoroutine(sceneName));
    }
    private IEnumerator SceneLoadCoroutine(string sceneName)
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
        SetFadeOut();
        yield return new WaitUntil(() => isFade == true);

        AsyncOperation load_op = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
        load_op.allowSceneActivation = false;

        yield return new WaitUntil(() => load_op.progress >= 0.9f);
        load_op.allowSceneActivation = true;

        yield return null;
        SceneManager.UnloadSceneAsync(currentSceneName);
        SetFadeIn();
        yield return new WaitUntil(() => isFade == false);

        yield break;
    }
    #endregion
}
