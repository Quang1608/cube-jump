using System.Collections;
using TMPro; // or use UnityEngine.UI.Text if not using TextMeshPro
using UnityEngine;

public class LevelIntroUI : MonoBehaviour
{
    public static LevelIntroUI Instance;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else { Destroy(gameObject); return; }

        // Start fully transparent
        SetAlpha(0f);
    }

    public TMP_Text levelText;       // assign in Inspector 
    public float displayTime = 2f;
    public float fadeDuration = 0.5f;

    public void ShowLevelText(string message)
    {
        levelText.text = message;
        StartCoroutine(ShowRoutine());
    }

    private IEnumerator ShowRoutine()
    {
        yield return Fade(0f, 1f); // fade in
        yield return new WaitForSeconds(displayTime);
        yield return Fade(1f, 0f); // fade out
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
        Color c = levelText.color;
        c.a = alpha;
        levelText.color = c;
    }
}