using UnityEngine;

public class DoorTrigger : MonoBehaviour
{
    public Door door;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if(Door.hasPlayerKey)
            {
                door.OpenDoor();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            door.CloseDoor();
        }
    }
}
