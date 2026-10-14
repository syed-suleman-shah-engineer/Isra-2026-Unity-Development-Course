using UnityEngine;

public class SettingsController : MonoBehaviour
{
    [SerializeField] private SettingsView view;
    [SerializeField] private AudioSource musicSource;

    public SettingsModel Model { get; private set; }

    private void Awake()
    {
        Model = new SettingsModel();                       // 1. create
    }

    private void Start()
    {
        view.MusicSliderMoved += OnMusicSliderMoved;       // View  -> Model
        Model.MusicVolumeChanged += view.ShowMusicVolume;  // Model -> View

        view.ShowMusicVolume(Model.MusicVolume);           // show the first value

        Model.MusicVolumeChanged += (value) => musicSource.volume = value;  // Model -> Audio
    }

    private void OnDestroy()
    {
        view.MusicSliderMoved -= OnMusicSliderMoved;
        Model.MusicVolumeChanged -= view.ShowMusicVolume;
        Model.MusicVolumeChanged -= (value) => musicSource.volume = value;
    }

    private void OnMusicSliderMoved(float value)
    {
        Model.MusicVolume = value;
    }
}