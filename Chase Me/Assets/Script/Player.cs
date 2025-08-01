using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody playerRb;
    private float speed = 10;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMovement();
    }

    void PlayerMovement()
    {
        float horinzontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(horinzontalInput, 0f, verticalInput);

        playerRb.linearVelocity = movement.normalized * speed + new Vector3(0, playerRb.linearVelocity.y, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PickUp"))
        {
            Destroy(other.gameObject);
        }
    }
}
