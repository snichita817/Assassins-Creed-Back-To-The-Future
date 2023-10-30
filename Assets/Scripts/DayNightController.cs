using UnityEngine;

public class DayNightController : MonoBehaviour
{
    public float dayDuration = 60f; // Duration of a full day in seconds
    public Light sun; // Reference to the Directional Light

    private float timeOfDay = 0f; // Current time of day (0-1)

    void Update()
    {
        // Update the time of day based on real-time
        timeOfDay += Time.deltaTime / dayDuration;

        // Ensure that timeOfDay is always within the 0-1 range
        timeOfDay %= 1.0f;

        // Set the sun's rotation to simulate day and night
        float angle = timeOfDay * 360f; // 0-360 degrees
        sun.transform.rotation = Quaternion.Euler(angle, 0, 0);
    }
}
