using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Rigidbody rigidBody;

    public float speed = 10f;
    public float health = 500f;
    public float moveDelay = 1f;

    private float lastAppliedForce;

    private void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
    }

    public void Damage()
    {
        health -= 50f;

        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        float move = Random.Range(-1f, 1f);

        if (Time.time - lastAppliedForce > moveDelay)
        {
            rigidBody.AddForce(transform.forward * move * speed);
            lastAppliedForce = Time.time;
        }
    }
}