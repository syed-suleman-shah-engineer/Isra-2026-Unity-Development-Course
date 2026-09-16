using UnityEngine;

public class Spinner : MonoBehaviour
{
    public float fanSpeed = 100f;

    public Transform fanParent;

    void FixedUpdate()
    {
        fanParent.Rotate(Vector3.up, fanSpeed * Time.fixedDeltaTime);
    }


    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            Debug.Log("Game Over");
            Time.timeScale = 0f;
        }
    }
}
