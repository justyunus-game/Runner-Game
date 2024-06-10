using UnityEngine;
using UnityEngine.UI;

public class CoinController : MonoBehaviour
{
    [SerializeField]
    private AudioSource coinSound;

    [SerializeField]
    private Text scoreText;

    public int scoreValue { get; private set; }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Coin"))
        {
            coinSound.Play();
            scoreValue++;
            scoreText.text = "Score: " + scoreValue;

            Destroy(other.gameObject);
        }
    }
}
