using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private SwipeControls swipeControls;

    [SerializeField]
    private GameObject menuPanel;

    [SerializeField]
    private GameObject closeMenuButton;

    private void Start()
    {
        swipeControls = FindAnyObjectByType<SwipeControls>();
        Time.timeScale = 1f;
    }
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void Play()
    {
        SceneManager.LoadScene(1);
    }

    public void Menu()
    {
        if (swipeControls.isPlaying)
        {
            menuPanel.SetActive(true);
            Time.timeScale = 0f;
        }
    }

    public void CloseMenu()
    {
        if (swipeControls.isPlaying)
        {
            menuPanel.SetActive(false);
            Time.timeScale = 1f;
        }
        else
        {
            if (!swipeControls.isPlaying)
            {
                closeMenuButton.SetActive(false);
            }
        }

    }
}
