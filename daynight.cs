using UnityEngine;

public class DayNightCycle : MonoBehaviour
{
    private Light directionalLight;
    public float cycleDuration = 24f; // Duration of the cycle in seconds
    public Color dayColor = Color.white;
    public Color nightColor = new Color(1f,0.5f,0f);
    public float maxIntensity = 1f;
    public float minIntensity = 0.2f;

    private float cycleProgress = 0f; // Progress of the cycle
    
    void Start()
    {
        directionalLight = GetComponent<Light>();
    }

    void Update()
    {

        // Update the cycle progress
        cycleProgress += Time.deltaTime / cycleDuration;
        cycleProgress %= 1f; // Keep the progress between 0 and 1

        // Calculate the rotation and intensity based on the cycle progress
        float angle = cycleProgress * 360f; // Full rotation over the cycle
        float intensityFactor = -1*Mathf.Cos(cycleProgress * Mathf.PI * 2); // Cosine wave for intensity

        // Apply rotation and intensity
        directionalLight.transform.rotation = Quaternion.Euler(new Vector3(angle - 90f, -30f, 0));
        directionalLight.intensity = Mathf.Lerp(minIntensity, maxIntensity, intensityFactor);

        // Interpolate light color based on day/night
        directionalLight.color = Color.Lerp(nightColor, dayColor, intensityFactor);
    }
}
