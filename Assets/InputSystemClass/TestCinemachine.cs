using Unity.Cinemachine;
using UnityEngine;

public class TestCinemachine : MonoBehaviour
{
    public CinemachineCamera playerCinemachine;
    public CinemachineCamera sphereCinemachine;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Invoke(nameof(SwitchFocusToSphereCamera), 2f);
    }

    private void SwitchFocusToSphereCamera()
    {
        playerCinemachine.Priority = 0;
        sphereCinemachine.Priority = 10;     
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    [ContextMenu("Switch to Player Camera")]
    public void SwitchToPlayerCamera()
    {
        playerCinemachine.Priority = 10;
        sphereCinemachine.Priority = 0;
    }
}
