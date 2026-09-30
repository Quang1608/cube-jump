using UnityEngine;

public class TowerManager : MonoBehaviour
{
    public static TowerManager Instance;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else { Destroy(gameObject); return; }
    }

    [Header("Tower Settings")]
    public int numberOfLevels = 5;
    public LevelData[] levelList;

    public LevelData currentLevel;
    private int currentLevelIndex = 0;

    public void StartTower()
    {
        LevelTimer.Instance.StartTimer();
    }

    public void UpdateLevel()
    {
        if (currentLevelIndex >= numberOfLevels)
        {
            EndTower();
            return;
        }

        if (currentLevel) {
            if (currentLevel.isBossFight) BossManager.Instance.EndBossFight();
        }

        currentLevelIndex++;
        currentLevel = SelectRandomLevel();

        if (currentLevel.isBossFight) BossManager.Instance.StartBossFight();
    }

    private void EndTower()
    {
        ResultScreen.Instance.ShowResults();
    }

    private LevelData SelectRandomLevel()
    {
        int randomIndex = Random.Range(0, levelList.Length);

        return levelList[randomIndex];
    }

    public LevelData GetCurrentLevel()
    {
        return currentLevel;
    }

    public int GetCurrentLevelIndex()
    {
        return currentLevelIndex;
    }
} 
