using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class ObstaclePusher : MonoBehaviour
{
    [SerializeField] private float force = 20f;

    private void Awake()
    {
        // OnTriggerStay only fires for triggers — enforce it.
        GetComponent<BoxCollider>().isTrigger = true;
    }

    // Called once per physics step, so ForceMode.Force is framerate-independent.
    private void OnTriggerStay(Collider other)
    {
        Rigidbody rb = other.attachedRigidbody;
        if (rb == null || rb.isKinematic) return;

        Vector3 dir = other.transform.position - transform.position;
        dir.y = 0f;
        dir.Normalize();
        rb.AddForce(dir * force, ForceMode.Acceleration);

        // ForceMode.Acceleration = mass-independent, so every object
        // gets pushed with the same "speed feel" regardless of weight.
        // rb.AddForce(transform.forward * force, ForceMode.Acceleration);
    }
}