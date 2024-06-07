using UnityEngine;
using UnityEngine.UI;

public class CoinController : MonoBehaviour
{
    [SerializeField]
    private Text scoreText;

    private int scoreValue;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Coin"))
        {
            scoreValue++;
            scoreText.text = "Score: " + scoreValue;

            Destroy(other.gameObject);
        }
    }
}
