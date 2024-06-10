using UnityEngine;

public class BackgroundMusicController : MonoBehaviour
{
    public static BackgroundMusicController instance;

    public AudioSource backgroundMusic;

    private bool isMuted = false;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void ToggleMusic()
    {
        isMuted = !isMuted;
        backgroundMusic.mute = isMuted;
    }

    public bool IsMuted()
    {
        return isMuted;
    }
}
