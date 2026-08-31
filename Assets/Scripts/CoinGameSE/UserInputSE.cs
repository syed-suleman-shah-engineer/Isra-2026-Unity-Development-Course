using UnityEngine;
using UnityEngine.InputSystem;

public class UserInputSE : MonoBehaviour
{
    [Header("References")]
    public BallMove ball;

    public InputActionAsset inputActionAsset;


    public void OnEnable()
    {
        if(inputActionAsset != null)
        {
            inputActionAsset.Enable();
        }
    }
    
    public void OnDisable()
    {
        if(inputActionAsset != null)
        {
            inputActionAsset.Disable();
        }
    }

    void Update()
    {
        Vector2 moveInput = inputActionAsset["Move"].ReadValue<Vector2>();
        ball.SetMoveInput(moveInput);
    }
}
