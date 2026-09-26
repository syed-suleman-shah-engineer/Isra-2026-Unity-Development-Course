using UnityEngine;

public class SlidingDoorAnimControl : MonoBehaviour
{
    public Animator animator;

    public bool isDoorOpened = false;

    public void Toggle()
    {
        isDoorOpened = !isDoorOpened;
        animator.SetBool("IsOn", isDoorOpened);
    }

    public void OpenDoor()
    {
        isDoorOpened = true;
        animator.SetBool("IsOn", isDoorOpened);
    }

    public void CloseDoor()
    {
        isDoorOpened = false;
        animator.SetBool("IsOn", false);
    }


    [ContextMenu("Test Toggle")]
    public void TestToggle()
    {
        Toggle();
    }


    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            OpenDoor();
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            CloseDoor();
        }
    }
}
