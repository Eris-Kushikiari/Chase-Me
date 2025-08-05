using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player : MonoBehaviour
{
    private Rigidbody playerRb;
    private float speed = 10;
    private float rotationSpeed = 3f;

    //water float
    public float underWaterDrag = 3f;
    public float underWaterAngularDrag = 1f;
    public float airDrag = 0f;
    public float airAngularDrag = 0.05f;
    public float floatingPower = 15f;
    public float waterLevel = 0f;
    private bool underWater;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        PlayerMovement();
        PlayerBuoyancy();
    }

    void PlayerMovement()
    {
        float horinzontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(horinzontalInput, 0f, verticalInput);
        Vector3 moveDirection = movement.normalized;

        playerRb.linearVelocity = moveDirection * speed + new Vector3(0, playerRb.linearVelocity.y, 0);

        //make the player face to its movement direction
        if (moveDirection.magnitude > 0.1f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PickUp"))
        {
            Destroy(other.gameObject);
        }
    }

    void PlayerBuoyancy()
    {
        float difference = transform.position.y - waterLevel;

        if (difference < 0)
        {
            playerRb.AddForceAtPosition(Vector3.up * floatingPower * Mathf.Abs(difference), transform.position, ForceMode.Force);
            if (!underWater)
            {
                underWater = true;
                SwitchState(true);
            }
        }
        else if (underWater)
        {
            underWater = false;
            SwitchState(false);
        }
    }

    void SwitchState(bool isUnderWater)
    {
        if (isUnderWater)
        {
            playerRb.linearDamping = underWaterDrag;
            playerRb.angularDamping = underWaterAngularDrag;
        }
        else
        {
            playerRb.linearDamping = airDrag;
            playerRb.angularDamping = airAngularDrag;
        }
    }

}
