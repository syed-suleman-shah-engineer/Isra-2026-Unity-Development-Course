using System;
using UnityEngine;

public class SettingsModel
{
    public event Action<float> MusicVolumeChanged;

    private float musicVolume = 0.8f;

    public float MusicVolume
    {
        get => musicVolume;
        set
        {
            value = Mathf.Clamp01(value);                         // rule: only 0 to 1
            if (Mathf.Approximately(musicVolume, value)) return;  // no change, no news
            musicVolume = value;
            MusicVolumeChanged?.Invoke(musicVolume);              // broadcast
        }
    }
}