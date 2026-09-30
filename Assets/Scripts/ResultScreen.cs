using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class ResultScreen : MonoBehaviour
{
    public static ResultScreen Instance;

    public GameObject resultPanel;   // the whole result screen UI, assign in Inspector
    public TMP_Text timeText;
    public TMP_Text livesText;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else { Destroy(gameObject); return; }

        if (resultPanel != null) resultPanel.SetActive(false);
    }

    public void ShowResults()
    {
        LevelTimer.Instance.StopTimer();
            
        float finalTime = LevelTimer.Instance.GetElapsedTime();
        int minutes = Mathf.FloorToInt(finalTime / 60f);
        int seconds = Mathf.FloorToInt(finalTime % 60f);
        int milliseconds = Mathf.FloorToInt((finalTime * 1000f) % 1000f);
        timeText.text = "Time: " + string.Format("{0:00}:{1:00}.{2:000}", minutes, seconds, milliseconds);

        // You'll need a way to read current lives — see note below
        livesText.text = "Lives Left: " + PlayerHealth.Instance.GetCurrentLives();

        resultPanel.SetActive(true);
        Time.timeScale = 0f; // pause game while viewing results
    }

    public void OnTryAgainPressed()
    {
        Time.timeScale = 1f; // unpause before reloading
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}