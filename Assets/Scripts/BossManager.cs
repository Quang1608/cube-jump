using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI;
using TMPro;

public class BossManager : MonoBehaviour
{
    public static BossManager Instance;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else { Destroy(gameObject); return; }
    }

    [Header("Boss Settings")]
    public GameObject BossBackground;
    public Image bossFillImage;
    public TMP_Text healthText;
    public int maxBossHealth = 100;
    public int currentBossHealth;

    [Header("Orb Spawn Settings")]
    public GameObject prefab;
    public Transform[] spawnPoints;

    [Range(1, 100)]
    public int amountToSpawn = 3;
    public int despawnTime = 5;
    public int spawningTime = 2;

    private float currentSpawnTime = 0f;
    private int currentOrbs = 0;
    private bool isBegun;

    List<Transform> availablePoints;

    private void Start()
    {
        //StartBossFight();
    }

    private void Update()
    {
        if (!isBegun) return;

        UpdateBossBar();

        if (currentBossHealth <= 0) EndBossFight();

        if (currentSpawnTime < spawningTime)
        {
            currentSpawnTime += Time.deltaTime;
            return;
        }

        currentSpawnTime = 0f;
        if (availablePoints.Count > 0 && currentOrbs < amountToSpawn) SpawnObject();
    }

    public void StartBossFight()
    {
        BossBackground.SetActive(true);
        currentBossHealth = maxBossHealth;
        availablePoints = new List<Transform>(spawnPoints);
        isBegun = true;
    }

    public void EndBossFight()
    {
        if (!isBegun) return;
        
        isBegun = false;
        BossBackground.SetActive(false);
        availablePoints.Clear();
        StartCoroutine(FadeManager.Instance.TeleportRoutine(false));
    }

    void SpawnObject()
    {
        int randomIndex = Random.Range(0, availablePoints.Count);

        Transform point = availablePoints[randomIndex];

        GameObject orb = Instantiate(prefab, point.position, point.rotation);

        availablePoints.RemoveAt(randomIndex);
        currentOrbs++;

        StartCoroutine(DestroyObject(orb, point)); 
    }

    private IEnumerator DestroyObject(GameObject orb, Transform removedPoint)
    {   
        float currentTime = 0f;

        while (currentTime < despawnTime)
        {
            //Debug.Log(currentTime);
            currentTime += Time.deltaTime;
            yield return null;
        }

        availablePoints.Add(removedPoint);
        Destroy(orb);
        currentOrbs--;
    }

    void UpdateBossBar()
    {
        bossFillImage.fillAmount = (float)currentBossHealth / maxBossHealth;

        healthText.text = currentBossHealth + " / " + maxBossHealth;
    }
}
