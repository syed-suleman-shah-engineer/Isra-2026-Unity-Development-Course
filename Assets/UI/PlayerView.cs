using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerView : MonoBehaviour
{
    public Image avatarImage;
    public TMP_Text nameText;
    public Slider healthSlider;
    public TMP_Text killsText, deathsText;


    public void ShowPlayerData(PlayerModel playerModel)
    {
        nameText.text = playerModel.Name;
        healthSlider.value = playerModel.Health;
        killsText.text = $"Kills: {playerModel.Kills}";
        deathsText.text = $"Deaths: {playerModel.Deaths}";

        if (playerModel.Avatar != null && playerModel.Avatar.Length > 0)
        {
            Texture2D texture = new Texture2D(2, 2);
            texture.LoadImage(playerModel.Avatar);
            avatarImage.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
        }
    }
}