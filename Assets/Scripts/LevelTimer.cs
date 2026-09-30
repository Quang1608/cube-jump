using UnityEngine;
using TMPro;

public class LevelTimer : MonoBehaviour
{
    public static LevelTimer Instance;

    public TMP_Text timerText;

    private float elapsedTime;
    private bool isRunning;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else { Destroy(gameObject); return; }
    }

    void Update()
    {
        if (!isRunning) return;

        elapsedTime += Time.deltaTime;
        UpdateUI();
    }

    public void StartTimer()
    {
        elapsedTime = 0f;
        isRunning = true;

        // transparent the timer text
        Color c = timerText.color;
        c.a = 1;
        timerText.color = c;
    }

    public void StopTimer()
    {
        isRunning = false;

        Color c = timerText.color;
        c.a = 0;
        timerText.color = c;
    }

    public float GetElapsedTime()
    {
        return elapsedTime;
    }

    private void UpdateUI()
    {
        int minutes = Mathf.FloorToInt(elapsedTime / 60f);
        int seconds = Mathf.FloorToInt(elapsedTime % 60f);
        int milliseconds = Mathf.FloorToInt((elapsedTime * 1000f) % 1000f);

        timerText.text = string.Format("{0:00}:{1:00}.{2:000}", minutes, seconds, milliseconds);
    }
}