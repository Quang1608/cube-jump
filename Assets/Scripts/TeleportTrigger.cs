using System.Collections;
using UnityEngine;

public class TeleportTrigger : MonoBehaviour
{
    public Vector3 destination;
    public bool isStarting;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (FadeManager.Instance.GetTeleportingState() || !other.CompareTag("Player")) return;

        StartCoroutine(FadeManager.Instance.TeleportRoutine(isStarting));
    }
}