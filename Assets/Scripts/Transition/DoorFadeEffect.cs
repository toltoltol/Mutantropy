using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DoorFadeEffect : MonoBehaviour
{
    public float fadeDuration = 1f;
    private Image fadePanel;

    void Awake()
    {
        fadePanel = GetComponentInChildren<Image>();
        fadePanel.color = new Color(0, 0, 0, 1);
    }

    public void FadeIn(System.Action onFadeComplete = null)
    {
        StartCoroutine(FadeRoutine(1f, 0f, onFadeComplete));
    }

    public void FadeOut(System.Action onFadeComplete = null)
    {
        StartCoroutine(FadeRoutine(0f, 1f, onFadeComplete));
    }

    private IEnumerator FadeRoutine(float startAlpha, float endAlpha, System.Action onFadeComplete)
    {
        float timer = 0f;
        Color currentColor = fadePanel.color;

        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            float alpha = Mathf.Lerp(startAlpha, endAlpha, timer / fadeDuration);
            fadePanel.color = new Color(0, 0, 0, alpha);
            yield return null;
        }

        fadePanel.color = new Color(0, 0, 0, endAlpha);
        if (onFadeComplete != null)
        {
            onFadeComplete();
        }
    }
}
