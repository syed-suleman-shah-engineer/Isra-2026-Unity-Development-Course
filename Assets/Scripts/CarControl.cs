using UnityEngine;

public class CarControl : MonoBehaviour
{
    public Rigidbody rigidBody;

    void Awake()
    {
        rigidBody = GetComponent<Rigidbody>();
    }

    public void ApplyForce(float force, Vector3 dir)
    {
        rigidBody.AddForce(dir * force);
    }
}
