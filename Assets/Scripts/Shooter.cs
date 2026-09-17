using UnityEngine;
using UnityEngine.InputSystem;

public class Shooter : MonoBehaviour
{
    public GameObject projectilePrefab;

    public Transform firePoint;


    public InputActionAsset fireAction;


    public void OnEnable()
    {
        fireAction.Enable();

        fireAction["Attack"].performed += ctx => FireProjectile();
    }

    public void OnDisable()
    {
        fireAction.Disable();

        fireAction["Attack"].performed -= ctx => FireProjectile();
    }


    private void FireProjectile()
    {
        if (projectilePrefab != null)
        {
            var projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);

            var rb = projectile.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddForce(firePoint.forward * 20f, ForceMode.Impulse);
            }
        }
    }

}
