using UnityEngine;

public class CoinSE : MonoBehaviour
{

    public float rotationSpeed = 100f;


    public void FixedUpdate()
    {
        transform.Rotate(Vector3.up, rotationSpeed * Time.fixedDeltaTime);
    }


    public void OnTriggerEnter(Collider other)
    {
        Debug.Log("Coin entered trigger with: " + other.name);

        if(other.CompareTag("Player"))
        {
            CoinManager.Instance.CollectCoin();
            gameObject.SetActive(false);
        }
    }

    public void OnTriggerExit(Collider other)
    {
        Debug.Log("Coin exited trigger with: " + other.name);
    }

    public void OnTriggerStay(Collider other)
    {
        Debug.Log("Coin staying in trigger with: " + other.name);
    }
}
