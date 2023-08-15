using InfimaGames.LowPolyShooterPack;
using UnityEngine;
using UnityEngine.SceneManagement;
using YG;

public class DieScreen : MonoBehaviour
{
    [Title(label: "Settings")]

    [Tooltip("Canvas to play animations on.")]
    [SerializeField]
    private GameObject animatedCanvas;

    [Tooltip("Animation played when showing this menu.")]
    [SerializeField]
    private AnimationClip animationShow;

    [Tooltip("Animation played when hiding this menu.")]
    [SerializeField]
    private AnimationClip animationHide;
    private Animation animationComponent;

    private void Start()
    {
        animatedCanvas.GetComponent<CanvasGroup>().alpha = 0;
        animationComponent = animatedCanvas.GetComponent<Animation>();
    }

    public void Show()
    {
        if(GameData.Instance.GetPauseVar())
        {
            GameData.Instance.pauseMenu.Hide();
        }
        
        YandexGame.FullscreenShow();
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        animationComponent.clip = animationShow;
        animationComponent.Play();
    }

    public void Hide()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        animationComponent.clip = animationHide;
        animationComponent.Play();
    }

    public void Restart()
    {
        GameData.Instance.LoadLevel(SceneManager.GetActiveScene().buildIndex);
    }

    public void Respawn()
    {
        YandexGame.RewVideoShow(0);
        Hide();
    }

    public void GoToMenu()
    {
        GameData.Instance.LoadLevel(0);
    }
}
