using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public int coinsCollected = 0;

    public void Awake()
    {
        if(Instance != null)
        {
            Destroy(gameObject);
        }

        Instance = this;

        coinsCollected = 0;
    }
}