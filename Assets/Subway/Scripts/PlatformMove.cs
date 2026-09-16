using UnityEngine;

public class PlatformMove : MonoBehaviour
{
    [Header("Platform Settings")]
    public float moveSpeed;
    public Vector3 destoryOnPosition;


    public void Initialize(float speed, Vector3 dPosition)
    {
        moveSpeed = speed;
        destoryOnPosition = dPosition;
    }


    public void FixedUpdate()
    {
        transform.position = transform.position + -transform.forward * moveSpeed * Time.fixedDeltaTime;

        if (transform.position.z < destoryOnPosition.z)
        {
            Destroy(gameObject);
        }
    }
}
