using System;
using UnityEngine;
using UnityEngine.UI;

public class SettingsView : MonoBehaviour
{
    [SerializeField] private Slider musicSlider;

    // The View reports what the user did. It does not decide anything.
    public event Action<float> MusicSliderMoved;

    private void Awake()
    {
        musicSlider.onValueChanged.AddListener(OnMusicSliderChanged);
    }

    private void OnDestroy()
    {
        musicSlider.onValueChanged.RemoveListener(OnMusicSliderChanged);
    }

    private void OnMusicSliderChanged(float value)
    {
        MusicSliderMoved?.Invoke(value);
    }

    public void ShowMusicVolume(float value)
    {
        musicSlider.SetValueWithoutNotify(value);
    }
}