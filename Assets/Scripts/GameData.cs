using InfimaGames.LowPolyShooterPack.Interface;
using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.SceneManagement;
using YG;

public class GameData : MonoBehaviour
{
    public static GameData Instance { get; private set; }

    [Title("Settings")]
    [SerializeField] private Animation transitionAnimation;
    public MenuQualitySettings pauseMenu { get; private set; }
    private PostProcessVolume pp; 

    private void OnEnable() => YandexGame.RewardVideoEvent += Rewarded;
    private void OnDisable() => YandexGame.RewardVideoEvent -= Rewarded;

    private void Awake()
    {
        Instance = this;
        pp = FindObjectOfType<PostProcessVolume>();
    }

    private void Update()
    {
        pp.enabled = YandexGame.savesData.ppEnabled;
    }

    public void LoadLevel(int level) => StartCoroutine(TransitionLevel(level));

    private IEnumerator TransitionLevel(int level)
    {
        transitionAnimation.Play();
        yield return new WaitForSeconds(transitionAnimation.clip.length);
        SceneManager.LoadScene(level);
    }

    private void Rewarded(int id)
    {
        if (id == 0)
            Health.Instance.ResetHealth();
    }

    public bool GetPauseVar()
    {
        if(pauseMenu == null)
        {
            pauseMenu = FindObjectOfType<MenuQualitySettings>();
        }

        return pauseMenu.IsPaused();
    }
}
