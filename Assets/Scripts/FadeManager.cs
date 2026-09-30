using System.Collections;
using UnityEngine;

public class FadeManager : MonoBehaviour
{
    public static FadeManager Instance;

    private GameObject playerObject;
    private Transform player;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else { Destroy(gameObject); return; }

        playerObject = GameObject.FindWithTag("Player");
        player = playerObject.transform;
    }

    public Vector3 destination;
    private float waitTime = 0.3f; // how long screen stays black before moving player

    private bool isTeleporting = false;

    public bool GetTeleportingState()
    {
        return isTeleporting;
    }

    public IEnumerator TeleportRoutine(bool isStarting)
    {
        isTeleporting = true;

        yield return ScreenTransitionManager.Instance.FadeOut(); // fade to black

        TowerManager.Instance.UpdateLevel();
        destination = TowerManager.Instance.GetCurrentLevel().levelDestination;
        PlayerHealth.Instance.respawnPoint = destination;

        if (isStarting) TowerManager.Instance.StartTower();

        // Move player while screen is black
        Rigidbody2D rb = player.GetComponent<Rigidbody2D>();
        if (rb != null) rb.position = destination;
        else player.position = destination;

        yield return new WaitForSeconds(waitTime);

        yield return ScreenTransitionManager.Instance.FadeIn(); // fade back in

        isTeleporting = false;

        string message = "Level " + TowerManager.Instance.GetCurrentLevelIndex();
        LevelIntroUI.Instance.ShowLevelText(message);
    }
}