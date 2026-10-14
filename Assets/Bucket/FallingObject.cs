using UnityEngine;

public class FallingObject : MonoBehaviour
{
    [Header("Settings")]
    public float fallingSpeed = 2f;
    private bool isDestroyed = false;


    [Header("Sfx")]
    public AudioClip inBucket, outBucket;


    private void Start()
    {
        isDestroyed = false;
    }


    private void FixedUpdate()
    {
        transform.position = transform.position + transform.up * fallingSpeed * -1f * Time.fixedDeltaTime;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (isDestroyed) return;

        if (other.CompareTag("Bucket"))
        {
            BucketGameManager.Instance.IncreaseScore();

            AudioSource.PlayClipAtPoint(inBucket, Camera.main.transform.position);
        }
        else
        {
            BucketGameManager.Instance.DecreaseScore();
            AudioSource.PlayClipAtPoint(outBucket, Camera.main.transform.position);
        }

        isDestroyed = true;
        Destroy(gameObject, .1f);
    }
}
