using UnityEngine;

public class PickUp : MonoBehaviour
{
    private float yRotation = 2;
    private float xRotation = 2;
    private float zRotation = 2;
    void Update()
    {
        transform.Rotate(xRotation, -yRotation, zRotation);
    }
}
