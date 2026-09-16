using UnityEngine;

public class TriggerTest : MonoBehaviour
{
    public Transform visualTransform;

    void FixedUpdate()
    {
        visualTransform.Rotate(Vector3.right, 100f * Time.fixedDeltaTime);
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.name + " Trigger Enter");
        gameObject.SetActive(false);
    }
}
