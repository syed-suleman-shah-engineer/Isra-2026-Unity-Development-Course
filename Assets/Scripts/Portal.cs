using UnityEngine;

public class Portal : MonoBehaviour
{
    public Transform destination;
    public Vector3 offset = new Vector3(0, 1, 1); // Adjust the offset as needed


    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            CharacterController controller = other.GetComponent<CharacterController>();
            if (controller != null)
            {
                controller.enabled = false;
                other.transform.position = destination.position + destination.forward + offset;
                controller.enabled = true;
            }
        }
    }

}
