using UnityEngine;

public class PlatformMover : MonoBehaviour
{
    public float speed = 2f; // Speed of the platform movement

    public void FixedUpdate()
    {
        transform.position += -Vector3.forward * speed * Time.fixedDeltaTime; // Move the platform backward along the Z-axis
    }
}
