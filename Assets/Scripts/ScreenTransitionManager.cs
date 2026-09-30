using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ScreenTransitionManager : MonoBehaviour
{
    public static ScreenTransitionManager Instance;

    public Image fadeImage;       // full-screen black Image, CanvasGroup or Image alpha
    public float fadeDuration = 0.5f;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else { Destroy(gameObject); return; }

        // Start fully transparent
        SetAlpha(0f);
    }

    public IEnumerator FadeOut() // black screen appears
    {
        yield return Fade(0f, 1f);
    }

    public IEnumerator FadeIn() // black screen disappears
    {
        yield return Fade(1f, 0f);
    }

    private IEnumerator Fade(float from, float to)
    {
        float elapsed = 0f;
        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            float alpha = Mathf.Lerp(from, to, elapsed / fadeDuration);
            SetAlpha(alpha);
            yield return null;
        }
        SetAlpha(to);
    }

    private void SetAlpha(float alpha)
    {
        Color c = fadeImage.color;
        c.a = alpha;
        fadeImage.color = c;
    }
}