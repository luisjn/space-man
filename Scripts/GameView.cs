using Enums;
using UnityEngine;
using UnityEngine.UI;

public class GameView : MonoBehaviour
{
    private static GameManager _gameManager;
    private PlayerController _playerController;
    
    public Text coinsText, maxScoreText, scoreText;
    
    private void Start()
    {
        _gameManager = GameManager.Instance;
        _playerController = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    private void Update()
    {
        if (_gameManager.currentGameState.Equals(GameState.InGame))
        {
            int coins = _gameManager.collectedObject;
            float score = _playerController.GetTravelledDistance();
            float maxScore = PlayerPrefs.GetFloat("maxscore", 0f);

            coinsText.text = coins.ToString();
            maxScoreText.text = "MaxScore: " + maxScore.ToString("f1");
            scoreText.text = "Score: " + score.ToString("f1");
        }
    }
}
