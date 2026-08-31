using UnityEngine;

public class BallMove : MonoBehaviour
{
    public Rigidbody rigidBody;

    public float moveSpeed = 15f;

    [Header("Inputs (ready-only)")]
    public Vector2 moveInput;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if(rigidBody == null)
        {
            rigidBody = GetComponent<Rigidbody>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        // Converting 2D move input to 3D movement vector
        Vector3 move = new Vector3(moveInput.x, 0, moveInput.y);

        // Moving the rigidbody based on the calculated movement vector
        rigidBody.MovePosition(rigidBody.position + move * moveSpeed * Time.fixedDeltaTime);
    }

    public void SetMoveInput(Vector2 input)
    {
        moveInput = input;
    }
}
