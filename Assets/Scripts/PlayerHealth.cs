using UnityEngine;
using TMPro;

public class PlayerHealth : MonoBehaviour
{
    public static PlayerHealth Instance;

    public int maxLives = 3;
    private int currentLives;

    public TMP_Text livesText;       // assign in Inspector
    public Vector3 respawnPoint;   // where player goes after losing a life
    public GameObject gameOverScreen; // simple panel with "Game Over" text, assign in Inspector

    void Awake()
    {
        if (Instance == null) Instance = this;
        else { Destroy(gameObject); return; }
    }

    void Start()
    {
        currentLives = maxLives;
        UpdateUI();
        if (gameOverScreen != null) gameOverScreen.SetActive(false);
    }

    public int GetCurrentLives()
    {
        return currentLives;
    }
    
    public void LoseLife()
    {
        currentLives--;
        UpdateUI();

        if (currentLives <= 0)
        {
            GameOver();
        }
        else
        {
            Respawn();
        }
    }

    private void Respawn()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.linearVelocity = Vector2.zero; // stop momentum before teleporting back
            rb.position = respawnPoint;
        }
        else
        {
            transform.position = respawnPoint;
        }
    }

    private void GameOver()
    {
        Debug.Log("Game Over");
        if (gameOverScreen != null) gameOverScreen.SetActive(true);
        Time.timeScale = 0f; // freezes the whole game (physics, animations, etc.)
    }

    private void UpdateUI()
    {
        if (livesText != null) livesText.text = "Life: " + currentLives;
    }
}