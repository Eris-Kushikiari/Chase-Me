using UnityEngine;

public class Buoyancy : MonoBehaviour
{
    public Rigidbody rb;
    //water float
    public float underWaterDrag = 3f;
    public float underWaterAngularDrag = 1f;
    public float airDrag = 0f;
    public float airAngularDrag = 0.05f;
    public float floatingPower = 15f;
    public float waterLevel = 0f;
    private bool underWater;


    // Update is called once per frame
    void Update()
    {
        BuoyancyEffect();
    }

    void BuoyancyEffect()
    {
        float difference = transform.position.y - waterLevel;

        if (difference < 0)
        {
            rb.AddForceAtPosition(Vector3.up * floatingPower * Mathf.Abs(difference), transform.position, ForceMode.Force);
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
            rb.linearDamping = underWaterDrag;
            rb.angularDamping = underWaterAngularDrag;
        }
        else
        {
            rb.linearDamping = airDrag;
            rb.angularDamping = airAngularDrag;
        }
    }
}
