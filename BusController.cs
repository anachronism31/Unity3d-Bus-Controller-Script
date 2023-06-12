using UnityEngine;

public class BusController : MonoBehaviour
{
    public float motorTorque = 5000f; // Motor torque
    private WheelCollider[] wheelColliders;

    private DepthOfField depthOfField; // Depth of Field effect reference
    public float speed = 50f; // Bus speed
    public float rotationSpeed = 100f; // Bus rotation speed
    public float maxRotationSpeed = 100f; // Maximum rotation speed
    public float maxSteerAngle = 30f; // Maximum steering angle
    public WheelCollider[] frontWheelColliders; // Front wheel colliders
    public WheelCollider[] rearWheelColliders; // Rear wheel colliders
    public Transform[] frontWheelTransforms; // Front wheel transforms
    public Transform[] rearWheelTransforms; // Rear wheel transforms

    private float horizontalInput; // Horizontal input value
    private float verticalInput; // Vertical input value
    private Quaternion[] frontWheelStartingRotations; // Front wheel starting rotations
    private Quaternion[] rearWheelStartingRotations; // Rear wheel starting rotations

    private void Start()
    {
        // Get Wheel Collider components
        wheelColliders = GetComponentsInChildren<WheelCollider>();

        // Save starting rotations
        frontWheelStartingRotations = new Quaternion[frontWheelTransforms.Length];
        rearWheelStartingRotations = new Quaternion[rearWheelTransforms.Length];

        for (int i = 0; i < frontWheelTransforms.Length; i++)
        {
            frontWheelStartingRotations[i] = frontWheelTransforms[i].localRotation;
        }

        for (int i = 0; i < rearWheelTransforms.Length; i++)
        {
            rearWheelStartingRotations[i] = rearWheelTransforms[i].localRotation;
        }
    }

    private void Update()
    {
        // Get user input
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
    }

    private void FixedUpdate()
    {
        // Control the forward-backward movement of the bus
        foreach (var collider in wheelColliders)
        {
            collider.motorTorque = Input.GetAxis("Vertical") * motorTorque;
        }

        // Control the forward-backward movement of the bus
        foreach (var collider in rearWheelColliders)
        {
            collider.motorTorque = verticalInput * speed;
        }

        // Control the steering of the bus
        foreach (var collider in frontWheelColliders)
        {
            float currentSteerAngle = Mathf.Lerp(0f, maxSteerAngle, Mathf.Abs(horizontalInput));
            collider.steerAngle = currentSteerAngle * Mathf.Sign(horizontalInput);
        }

        // Rotate the wheels
        UpdateWheelPoses();

        // Adjust the wheel rotation speed based on the speed of the vehicle
        float currentRotationSpeed = Mathf.Lerp(rotationSpeed, maxRotationSpeed, GetComponent<Rigidbody>().velocity.magnitude / speed);
        rotationSpeed = currentRotationSpeed;
    }

    private void UpdateWheelPoses()
    {
        // Update the rotations of the front wheels
        for (int i = 0; i < frontWheelColliders.Length; i++)
        {
            UpdateWheelPose(frontWheelColliders[i], frontWheelTransforms[i], frontWheelStartingRotations[i]);
        }

        // Update the rotations of the rear wheels
        for (int i = 0; i < rearWheelColliders.Length; i++)
        {
            UpdateWheelPose(rearWheelColliders[i], rearWheelTransforms[i], rearWheelStartingRotations[i]);
        }
    }

    private void UpdateWheelPose(WheelCollider wheelCollider, Transform wheelTransform, Quaternion startingRotation)
    {
        // Update the wheel transform
        Vector3 position;
        Quaternion rotation;
        wheelCollider.GetWorldPose(out position, out rotation);
        wheelTransform.position = position;
        wheelTransform.rotation = rotation * startingRotation;
    }
}
//anachronism31 - github
