using UnityEngine;

public class KeyTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Door.hasPlayerKey = true;
            gameObject.SetActive(false);
        }
    }
}