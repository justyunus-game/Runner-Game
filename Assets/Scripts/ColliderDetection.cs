using UnityEngine;

public class ColliderDetection : MonoBehaviour
{
    private SwipeControls swipeControls;

    public GameObject gameOverPanel;

    private void Start()
    {
        swipeControls = FindAnyObjectByType<SwipeControls>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Obstacle"))
        {
            gameOverPanel.SetActive(true);

            Time.timeScale = 0f;

            swipeControls.isPlaying = false;
        }
    }
}

