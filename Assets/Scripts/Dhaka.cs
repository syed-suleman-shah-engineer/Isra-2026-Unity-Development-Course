using UnityEngine;


public class Dhaka : MonoBehaviour
{
    public float dhakaPower = 500f;

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("collided with "+other.gameObject.name);
        var car = other.gameObject.transform.parent.GetComponent<CarControl>();

        if(car == null) return;

        Debug.Log("Applying Force");
        car.ApplyForce(dhakaPower, transform.forward);
    }
}