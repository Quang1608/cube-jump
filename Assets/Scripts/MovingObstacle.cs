using UnityEngine;
using System.Collections;
using UnityEditor;

public class MovingObstacle : MonoBehaviour
{
    
    public float moveDuration = 3f;
    public float moveDistance = 5f;
    public Vector3 unitVector;
    public bool movingForward = true;
    private bool isMoving = false;
    private Vector3 initPosition;
    private Vector3 destination;

    // Update is called once per frame
    void Update()
    {
        if (!isMoving) StartCoroutine(MovePlatform());
    }

    private IEnumerator MovePlatform()
    {
        initPosition = transform.position;
        isMoving = true;

        if (movingForward) yield return Transition(initPosition, initPosition + unitVector * moveDistance);
        else yield return Transition(initPosition, initPosition - unitVector * moveDistance);

        movingForward = !movingForward;
        isMoving = false;
    }
    private IEnumerator Transition(Vector3 from, Vector3 to)
    {
        float elapsed = 0f;

        while (elapsed < moveDuration)
        {
            elapsed += Time.deltaTime;
            
            float goalx = Mathf.Lerp(from.x, to.x , elapsed / moveDuration);
            float goaly = Mathf.Lerp(from.y, to.y , elapsed / moveDuration);
            transform.position = new Vector3(goalx, goaly);
            
            yield return null;
        }

        transform.position = new Vector3(to.x, to.y);
    }
}
