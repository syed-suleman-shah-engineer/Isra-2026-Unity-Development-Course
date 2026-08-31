using UnityEngine;

public class Chair : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    [ContextMenu("Move Right")]
    public void MoveRight()
    {
        transform.position += transform.right * 2f;
    }
}
