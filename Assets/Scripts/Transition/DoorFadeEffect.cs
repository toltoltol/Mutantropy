using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DoorFadeEffect : MonoBehaviour
{
    public float fadeDuration = 0.2f;
    private Image fadePanel;
    private CanvasGroup canvasGroup;

    void Awake()
    {
        fadePanel = GetComponentInChildren<Image>();
        canvasGroup = GetComponentInChildren<CanvasGroup>();
        canvasGroup.alpha = 1f;
    }

    public void FadeIn(System.Action onFadeComplete = null)
    {
        StartCoroutine(FadeRoutine(1f, 0f, onFadeComplete));
    }

    public void FadeOut(System.Action onFadeComplete = null)
    {
        StartCoroutine(FadeRoutine(1f, 1f, onFadeComplete));
    }

    private IEnumerator FadeRoutine(float startAlpha, float endAlpha, System.Action onFadeComplete)
    {
        float timer = 0f;
        Color currentColor = fadePanel.color;

        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            float alpha = Mathf.Lerp(startAlpha, endAlpha, timer / fadeDuration);
            canvasGroup.alpha = alpha;
            yield return null;
        }

        canvasGroup.alpha = endAlpha;
        if (onFadeComplete != null)
        {
            onFadeComplete();
        }
    }
}
