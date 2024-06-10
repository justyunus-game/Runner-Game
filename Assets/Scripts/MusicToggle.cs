using UnityEngine;
using UnityEngine.UI;

public class MusicToggle : MonoBehaviour
{
    private BackgroundMusicController backgroundMusicController;

    [SerializeField]
    private Button toggleButton;

    [SerializeField]
    private Sprite musicOnIcon;

    [SerializeField]
    private Sprite musicOffIcon;

    private bool isMusicOn = true;

    private void Start()
    {
        backgroundMusicController = BackgroundMusicController.instance;

        if (backgroundMusicController == null)
        {
            Debug.LogError("BackgroundMusicController bulunamadý!");
            return;
        }

        UpdateButtonIcon();
    }

    public void ToggleMusic()
    {
        isMusicOn = !isMusicOn;
        backgroundMusicController.ToggleMusic();
        UpdateButtonIcon();
    }

    private void UpdateButtonIcon()
    {
        if (isMusicOn)
        {
            toggleButton.image.sprite = musicOnIcon;
        }
        else
        {
            toggleButton.image.sprite = musicOffIcon;
        }
    }
}
