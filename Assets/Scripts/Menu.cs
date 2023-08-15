using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using YG;

public class Menu : MonoBehaviour
{
    [Title("Menu")]
    [SerializeField] private GameObject settingsPage;

    [Title("Settings")]
    [SerializeField] private Toggle music;
    [SerializeField] private Toggle pp;
    [SerializeField] private Slider volume;
    [SerializeField] private Slider sensitivity;
    private AudioSource audioSource;
    private void OnEnable()
    {
        YandexGame.GetDataEvent += Load;
    }

    private void OnDisable()
    {
        YandexGame.GetDataEvent -= Load;
    }

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();

        if (YandexGame.SDKEnabled)
        {
            Load();
        }
    }

    private void Load()
    {
        music.isOn = YandexGame.savesData.musicEnabled;
        pp.isOn = YandexGame.savesData.ppEnabled;
        volume.value = YandexGame.savesData.volumeValue;
        sensitivity.value = YandexGame.savesData.sensitivityValue;
        audioSource.enabled = true;
    }

    public void ToggleSettings() => settingsPage.SetActive(!settingsPage.activeSelf);

    public void SaveSettings()
    {
        YandexGame.savesData.musicEnabled = music.isOn;
        YandexGame.savesData.ppEnabled = pp.isOn;
        YandexGame.savesData.volumeValue = volume.value;
        YandexGame.savesData.sensitivityValue = sensitivity.value;
        YandexGame.SaveProgress();

        AudioListener.volume = volume.value;
    }

    public void Play()
    {
        GameData.Instance.LoadLevel(1);
    }
}
