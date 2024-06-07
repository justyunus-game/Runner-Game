using UnityEngine;

public class ColliderDetection : MonoBehaviour
{
    public GameObject gameOverPanel;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Obstacle"))
        {
            gameOverPanel.SetActive(true);

            Time.timeScale = 0f;
        }
    }
}

