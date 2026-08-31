using UnityEngine;

public class Coin : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.name+" Trigger Enter");

        GameManager.Instance.coinsCollected += 1;
        
        gameObject.SetActive(false);
    }
}