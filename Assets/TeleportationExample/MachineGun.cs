using UnityEngine;

public class MachineGun : MonoBehaviour
{
    [Header("Rotation Settings")]
    public float turretRotationSpeed = 5f;
    public Transform turretTransform;

    [Header("Projectile Settings")]
    public Projectile projectilePrefab;
    public Transform projectileSpawnPoint;
    public float launchForce = 90f;

    public float targetAngle = 5f;

    public float shootDelay = 2f;

    private float _shootAt; // time when it has shot the projectile.


    public bool IsShootReady => (Time.time - _shootAt) > shootDelay;

    // public float startRotatingDelay = 1.5f;

    private Transform target;


    public void FixedUpdate()
    {
        if (target == null) return;

        // rotate towards target
        if (IsShootReady)
        {
            Vector3 direction = target.position - turretTransform.position;
            direction.y = 0f; // Keep the turret rotation on the horizontal plane
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            turretTransform.rotation = Quaternion.Slerp(turretTransform.rotation, lookRotation, turretRotationSpeed * Time.deltaTime);

            // if gun is facing to the player then shoot
            // calculate the angle
            float angleToTarget = Vector3.Angle(turretTransform.forward, direction);

            if (angleToTarget <= targetAngle)
            {
                Shoot();
            }
        }

    }

    private void Shoot()
    {
        var projectile = Instantiate(projectilePrefab, projectileSpawnPoint.position, projectileSpawnPoint.rotation);
        projectile.Launch(projectileSpawnPoint.forward, launchForce);

        _shootAt = Time.time;
    }



    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            target = other.transform;
        }
    }


    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            target = null;
        }
    }
}
