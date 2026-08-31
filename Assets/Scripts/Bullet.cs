using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed = 20f;

    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void Launch(Vector3 direction)
    {
        direction = direction.normalized;

        rb.linearVelocity = direction * speed;

        transform.forward = direction;
    }

    /// <summary>
    /// OnTriggerEnter is called when the Collider other enters the trigger.
    /// </summary>
    /// <param name="other">The other Collider involved in this collision.</param>
    void OnTriggerEnter(Collider other)
    {
        Debug.Log("has triggered with "+other.gameObject.name);

        var enemy = other.gameObject.transform.root.GetComponent<Enemy>();

        if(enemy != null)
        {
            Debug.Log("Damage Given");
            enemy.Damage();
        }

        Destroy(gameObject);
    }
}