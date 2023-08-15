using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YG;

public class Audio : MonoBehaviour
{
    [Title("Music")]
    [SerializeField] private float musicEndTime;
    [SerializeField] private AudioSource music;
    private float musicVolume;

    private void Start()
    {
        StartCoroutine(MusicTime(musicEndTime));
    }

    private void Update()
    {
        if(GameData.Instance.GetPauseVar() || Health.Instance.died) musicVolume = 0f;
        else musicVolume = 0.1f;

        AudioListener.volume = YandexGame.savesData.volumeValue;
        music.enabled = YandexGame.savesData.musicEnabled;
        music.volume = Mathf.Lerp(music.volume, musicVolume, 5f * Time.deltaTime);
    }

    private IEnumerator MusicTime(float time)
    {
        music.PlayOneShot(music.clip);

        while (true)
        {
            yield return new WaitForSeconds(time);
            music.PlayOneShot(music.clip);
        }
    }
}
