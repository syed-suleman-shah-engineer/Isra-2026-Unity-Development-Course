using UnityEngine;

public class TriggerEvent : MonoBehaviour
{
    public LayerMask triggerLayerMask; // Layer mask to filter which objects can trigger the event
    public UnityEngine.Events.UnityEvent<GameObject> onTriggerEnter; // Event to invoke when an object enters the trigger

    private void OnTriggerEnter(Collider other)
    {
        if ((triggerLayerMask.value & (1 << other.gameObject.layer)) != 0)
        {
            onTriggerEnter?.Invoke(other.gameObject);
        }
    }
}