using UnityEngine;
using UnityEngine.UI;

public class HighScoreManager : MonoBehaviour
{
    [SerializeField]
    private Text highScoreText;

    [SerializeField]
    private CoinController coinController;

    private int highScoreValue;

    private void Start()
    {
        if (coinController == null)
        {
            coinController = FindAnyObjectByType<CoinController>();
        }

        highScoreValue = PlayerPrefs.GetInt("HighScore");
        highScoreText.text = "High Score: " + highScoreValue;

        if (coinController != null && coinController.scoreValue > highScoreValue)
        {
            highScoreValue = coinController.scoreValue;
            highScoreText.text = "High Score: " + highScoreValue;
            PlayerPrefs.SetInt("HighScore", highScoreValue);
        }
    }

    private void Update()
    {
        if (coinController != null && coinController.scoreValue > highScoreValue)
        {
            highScoreValue = coinController.scoreValue;
            highScoreText.text = "High Score: " + highScoreValue;
            PlayerPrefs.SetInt("HighScore", highScoreValue);
            PlayerPrefs.Save();
        }
    }
}