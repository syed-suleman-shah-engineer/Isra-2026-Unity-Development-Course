using UnityEngine;

public class Door : MonoBehaviour
{
    public Vector3 openRotation;
    public Vector3 closeRotation;

    public static bool hasPlayerKey = false;
    
    public void OpenDoor()
    {
        transform.localEulerAngles = openRotation;
    }


    public void CloseDoor()
    {
        transform.localEulerAngles = closeRotation;
    }
}
