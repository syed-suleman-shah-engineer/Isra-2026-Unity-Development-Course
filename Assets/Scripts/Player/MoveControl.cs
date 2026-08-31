using UnityEngine;
using UnityEngine.InputSystem;

public class MoveControl : MonoBehaviour
{
    [Header("Reference")]
    public Rigidbody rigidBody;

    [Header("Move Settings")]
    public float moveSpeed = 10f;

    [Header("Runtime (Read-Only)")]
    public Vector2 moveDir;

    void Start()
    {
        if(rigidBody == null)
        {
            rigidBody = GetComponent<Rigidbody>();
        }
    }

    void FixedUpdate()
    {
        var moveDirection = new Vector3(moveDir.x, 0, moveDir.y);
        rigidBody.MovePosition(rigidBody.position + moveDirection * moveSpeed * Time.fixedDeltaTime);
    }

    public void Move(Vector2 direction)
    {
        moveDir = direction;
    }
}