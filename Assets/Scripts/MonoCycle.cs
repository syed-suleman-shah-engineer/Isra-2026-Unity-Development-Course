using UnityEngine;

public class MonoCycle : MonoBehaviour
{
    public float example = 5;

    void Awake()
    {
        Debug.Log("Awake "+gameObject.name);
    }

    void OnEnable()
    {
        Debug.Log("Enable "+gameObject.name);
    }

    void OnDisable()
    {
        Debug.Log("Disable");
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Debug.Log("Start");
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("Update");
    }


    void FixedUpdate()
    {
        Debug.Log("FixedUpdate");
    }

    void LateUpdate()
    {
        Debug.Log("LateUpdate");
    }

    void OnDestroy()
    {
        Debug.Log("Destroy");
    } 

    /// <summary>
    /// Reset is called when the user hits the Reset button in the Inspector's
    /// context menu or when adding the component the first time.
    /// </summary>
    void Reset()
    {
        example = 5;
    }
}
