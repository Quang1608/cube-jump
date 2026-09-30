using UnityEngine;

public class OrbCollection : MonoBehaviour
{
    public Collider2D collision;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        collision = GetComponent<Collider2D>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        BossManager.Instance.currentBossHealth -= 5;

        Destroy(gameObject);
    }
}
