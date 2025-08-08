using UnityEngine;

public class PickUp : MonoBehaviour
{
    private float yRotation = 2;
    private float xRotation = 0;
    private float zRotation = 0;
    void Update()
    {
        transform.Rotate(xRotation, yRotation, zRotation);
    }
}
