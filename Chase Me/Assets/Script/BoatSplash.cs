using UnityEngine;

public class BoatSplash : MonoBehaviour
{
    public GameObject splashWater;
    public Transform leftSplashPoint;
    public Transform rightSplashPoint;
    public float speedThreshold = 1f;
    public float waterLevel = 0f;
    public float splashCooldown = 0.1f;

    private Rigidbody rb;
    private float lastSplashTime;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float speed = rb.linearVelocity.magnitude;

        bool isInWater = transform.position.y < waterLevel + 1f;

        if (isInWater && speed > speedThreshold && Time.time > lastSplashTime + splashCooldown)
        {
            Instantiate(splashWater, leftSplashPoint.position, Quaternion.identity);
            Instantiate(splashWater, rightSplashPoint.position, Quaternion.identity);
            lastSplashTime = Time.time;
        }
    }
}
