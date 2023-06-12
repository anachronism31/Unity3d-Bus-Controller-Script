using UnityEngine;

public class BrakeLight : MonoBehaviour
{
    public Light[] brakeLights; // Rear brake lights (Point Lights)

    private bool isBraking = false; // Is the vehicle braking?

    private void Start()
    {
        // Turn off the brake lights at the start
        foreach (Light brakeLight in brakeLights)
        {
            brakeLight.enabled = false;
        }
    }

    private void Update()
    {
        // Check if the vehicle is braking
        if (Input.GetKey(KeyCode.Space)) // Brake input key (e.g., "Space" key)
        {
            isBraking = true;
        }
        else
        {
            isBraking = false;
        }

        // Update the brake lights
        UpdateBrakeLights();
    }

    private void UpdateBrakeLights()
    {
        // Update the state of the brake lights
        foreach (Light brakeLight in brakeLights)
        {
            brakeLight.enabled = isBraking; // Turn on the lights if braking, turn off otherwise
        }
    }
}


