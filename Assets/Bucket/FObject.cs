using UnityEngine;

public class FObject : MonoBehaviour
{
    [Header("Speed")]
    public float fallingSpeed = 0.5f;


    [Header("Audio Clips")]
    public AudioClip inB, outB;


    private void FixedUpdate()
    {
        transform.position = transform.position + transform.up * (fallingSpeed * -1) * Time.fixedDeltaTime;
    }


    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bucket"))
        {
            // score ++
            BManager.Instance.AddScore();
            AudioSource.PlayClipAtPoint(inB, Camera.main.transform.position);
        }
        else
        {
            // score --
            BManager.Instance.MinusScore();
            AudioSource.PlayClipAtPoint(outB, Camera.main.transform.position);
        }

        Destroy(gameObject);
    }
}
