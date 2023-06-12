using UnityEngine;

public class SpeedBasedCamera : MonoBehaviour
{
    public Transform target; // The target vehicle object to follow
    public Vector3 offset = new Vector3(0f, 2f, -5f); // The distance and height offset between the camera and the vehicle
    public float minDistance = 5f; // Minimum camera distance
    public float maxDistance = 10f; // Maximum camera distance
    public float distanceSmoothSpeed = 2f; // Smooth transition speed for the camera distance
    public float followSpeed = 5f; // Camera follow speed

    private Vector3 desiredPosition; // The desired position for the camera
    private float currentDistance; // The current camera distance
    private float targetDistance; // The target camera distance
    private float currentSpeed; // The current vehicle speed

    private void Start()
    {
        currentDistance = (minDistance + maxDistance) / 2f;
        targetDistance = currentDistance;
    }

    private void LateUpdate()
    {
        // Get the vehicle speed
        currentSpeed = target.GetComponent<Rigidbody>().velocity.magnitude;

        // Calculate the desired position
        desiredPosition = target.position + target.TransformDirection(offset) * currentDistance;

        // Set the follow factor for camera movement speed
        float followFactor = followSpeed / currentDistance;

        // Smoothly follow the desired position of the camera
        transform.position = Vector3.Lerp(transform.position, desiredPosition, Time.deltaTime * followFactor);

        // Adjust the camera distance based on the vehicle speed
        targetDistance = Mathf.Lerp(minDistance, maxDistance, currentSpeed / followSpeed);
        currentDistance = Mathf.Lerp(currentDistance, targetDistance, Time.deltaTime * distanceSmoothSpeed);

        // Look at the target
        transform.LookAt(target);
    }
}



