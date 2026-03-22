using UnityEngine;

public class FlightController : MonoBehaviour
{
    [SerializeField] private float pitchSpeed  = 45f;  // degrees/second 
    [SerializeField] private float yawSpeed    = 45f;  // degrees/second 
    [SerializeField] private float rollSpeed   = 45f;  // degrees/second 
    [SerializeField] private float thrustSpeed = 5f;   // units/second

    // TODO (Task 3-A)

    private Rigidbody rb;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // TODO (Task 3-B)
        rb = GetComponent<Rigidbody>(); 
        rb.freezeRotation = true;
    }

    // Update is called once per frame
    void Update()
    {
        HandleRotation(); 
        HandleThrust(); 
    }

    private void HandleRotation() 
{
    float pitchInput = Input.GetAxis("Vertical");
    transform.Rotate(Vector3.right * pitchInput * pitchSpeed * Time.deltaTime);

    float yawInput = Input.GetAxis("Horizontal");
    transform.Rotate(Vector3.up * yawInput * yawSpeed * Time.deltaTime);

    float rollInput = 0f;
    if (Input.GetKey(KeyCode.Q)) rollInput = 1f;
    else if (Input.GetKey(KeyCode.E)) rollInput = -1f;
    transform.Rotate(Vector3.forward * rollInput * rollSpeed * Time.deltaTime);
}
private void HandleThrust() 
{
    if (Input.GetKey(KeyCode.Space)) 
    {
        transform.Translate(Vector3.forward * thrustSpeed * Time.deltaTime);
    }
}
}
