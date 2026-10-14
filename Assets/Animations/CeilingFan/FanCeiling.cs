using UnityEngine;

public class FanCeiling : MonoBehaviour
{
    public Animator fanAnimator;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            fanAnimator.SetBool("IsOn", true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            fanAnimator.SetBool("IsOn", false);
        }
    }
}
