using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Rigidbody rb;

    public void Launch(Vector3 direction, float force)
    {
        rb.AddForce(direction * force, ForceMode.Impulse);
    }


    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Time.timeScale = 0f; // Pause the game
            Debug.Log("Game Over! Player hit by projectile.");
        }
    }
}
