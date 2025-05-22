using System.Collections;
using UnityEngine;

public class UIFader : MonoBehaviour
{
    private CanvasGroup canvasGroup;
    private float fadeDuration = 1f;

    public void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        FadeIn();
    }

    void Reset()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void FadeIn()
    {
        gameObject.SetActive(true);
        StartCoroutine(Fade(0f, 1f));
    }

    public void FadeOut()
    {
        StartCoroutine(Fade(1f, 0f, disableAtEnd: true));
    }

    private IEnumerator Fade(float start, float end, bool disableAtEnd = false)
    {
        float elapsed = 0f;
        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(start, end, elapsed / fadeDuration);
            yield return null;
        }
        canvasGroup.alpha = end;

        if (disableAtEnd && end == 0f)
            gameObject.SetActive(false);
    }
}
