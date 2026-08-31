using UnityEngine;
using UnityEngine.InputSystem;

public class UserInput : MonoBehaviour
{
    public InputActionAsset inputActionAsset;

    public MoveControl moveControl;


    void OnEnable()
    {
        inputActionAsset.Enable();
    }

    void OnDisable()
    {
        inputActionAsset.Disable();
    }


    void Update()
    {
        var moveDir = inputActionAsset["Move"].ReadValue<Vector2>();

        if(moveControl != null)
        {
            moveControl.Move(moveDir);
        }
    }
}