using UnityEngine;

public class DayNightCycle : MonoBehaviour
{
    [Header("Day Duration Settings")]
    [Tooltip("The number of seconds it takes for a full day to pass.")]
    public float dayDurationInSeconds = 120f;

    private float rotationSpeed;

    void Start()
    {
        // Calculate how many degrees to rotate per second
        rotationSpeed = 360f / dayDurationInSeconds;
    }

    void Update()
    {
        // Rotate the light around the X-axis to simulate the sun's movement
        transform.Rotate(Vector3.right * rotationSpeed * Time.deltaTime);
    }
}
