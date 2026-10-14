using UnityEngine;

public class DoorPuzzle : MonoBehaviour
{
    public GameObject keyObject;

    public Animator doorAnimator;

    private GameObject objectAttached;

    public void DoorOpen(bool isOpen = true)
    {
        doorAnimator.SetBool("IsOn", isOpen);
    }


    private void FixedUpdate()
    {
        if (objectAttached != null)
        {
            DoorOpen();
        }
        else
        {
            DoorOpen(false);
        }
    }


    public void OnTriggerStay(Collider other)
    {
        if (other.gameObject == keyObject)
        {
            objectAttached = other.gameObject;
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject == keyObject)
        {
            objectAttached = null;
        }
    }


}
