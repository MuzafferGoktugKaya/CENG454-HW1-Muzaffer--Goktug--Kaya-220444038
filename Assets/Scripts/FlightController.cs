using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class FlightController : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float pitchSpeed  = 45f;
    [SerializeField] private float yawSpeed    = 45f;
    [SerializeField] private float rollSpeed   = 45f;
    [SerializeField] private float thrustSpeed = 15f; // Hızı biraz artırdık

    private Rigidbody rb;
    private float currentThrust = 0f;

    void Start()
    {
        rb = GetComponent<Rigidbody>(); 
        rb.useGravity = false; // Uçuş modunda yerçekimini kapatalım
        rb.freezeRotation = true; // Fizik motorunun rotasyonu bozmasını engelle
    }

    void Update()
    {
        HandleRotation(); 
        HandleThrust(); 
    }

    private void HandleRotation() 
    {
        // GetAxis yerine GetAxisRaw kullanarak daha keskin ama yumuşatılabilir veri alıyoruz
        float pitchInput = Input.GetAxis("Vertical");
        float yawInput = Input.GetAxis("Horizontal");
        
        float rollInput = 0f;
        if (Input.GetKey(KeyCode.Q)) rollInput = 1f;
        else if (Input.GetKey(KeyCode.E)) rollInput = -1f;

        // Rotasyonları uygula
        transform.Rotate(Vector3.right * pitchInput * pitchSpeed * Time.deltaTime);
        transform.Rotate(Vector3.up * yawInput * yawSpeed * Time.deltaTime);
        transform.Rotate(Vector3.forward * rollInput * rollSpeed * Time.deltaTime);
    }

    private void HandleThrust() 
    {
        // Space'e basılı tutunca hız yavaşça artsın (Daha gerçekçi gaz verme)
        if (Input.GetKey(KeyCode.Space)) 
        {
            currentThrust = Mathf.Lerp(currentThrust, thrustSpeed, Time.deltaTime);
        }
        else 
        {
            // Bırakınca yavaşça süzülerek dursun
            currentThrust = Mathf.Lerp(currentThrust, 0f, Time.deltaTime * 0.5f);
        }

        transform.Translate(Vector3.forward * currentThrust * Time.deltaTime);
    }
}