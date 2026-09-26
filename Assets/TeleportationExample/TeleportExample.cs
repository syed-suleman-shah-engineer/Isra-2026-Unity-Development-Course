using UnityEngine;

public class TeleportExample : MonoBehaviour
{
    public Transform destinationTransform;
    public Vector3 offset;

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.gameObject.SetActive(false);

            other.transform.position = destinationTransform.position + destinationTransform.forward + offset;
            
            other.gameObject.SetActive(true);
        }
    }
}
