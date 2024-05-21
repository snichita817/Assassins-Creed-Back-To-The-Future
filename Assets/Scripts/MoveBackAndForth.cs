using UnityEngine;

public class MoveBackAndForth : MonoBehaviour
{
    public float speed = 1.0f; // Speed of the movement
    public float distance = 6.0f; // Distance to move back and forth

    private Vector3 startPosition;
    private Vector3 targetPosition;
    private float startTime;
    private bool movingForward = true;

    void Start()
    {
        startPosition = transform.position;
        targetPosition = startPosition + new Vector3(0, 0, distance);
        startTime = Time.time;
    }

    void Update()
    {
        float journeyLength = Vector3.Distance(startPosition, targetPosition);
        float distCovered = (Time.time - startTime) * speed;
        float fracJourney = distCovered / journeyLength;

        // Apply easing function
        float easeFracJourney = EaseInOut(fracJourney);

        // Move the object
        transform.position = Vector3.Lerp(startPosition, targetPosition, easeFracJourney);

        // Check if the object reached the target
        if (fracJourney >= 1.0f)
        {
            // Toggle the direction
            movingForward = !movingForward;
            startTime = Time.time;

            // Swap start and target positions
            Vector3 temp = startPosition;
            startPosition = targetPosition;
            targetPosition = temp + new Vector3(0, 0, movingForward ? distance : -distance);
        }
    }

    // Ease in and ease out function
    float EaseInOut(float t)
    {
        return t * t * (3f - 2f * t);
    }
}
