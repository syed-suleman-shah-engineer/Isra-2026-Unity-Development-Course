using TMPro;
using UnityEngine;

public class CoinManager : MonoBehaviour
{
    public static CoinManager Instance;

    public TMP_Text coinText;

    public int coinCount = 0;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void CollectCoin()
    {
        coinCount++;
        coinText.text = "Coins: " + coinCount;
    }
}
