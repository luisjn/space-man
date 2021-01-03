using System;
using Enums;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static LevelManager _levelManager;
    private static MenuManager _menuManager;
    
    private PlayerController _playerController;
    public static GameManager Instance { get; private set; }
    
    public GameState currentGameState;
    public int collectedObject = 0;
    
    private void Awake()
    {
        if (Instance == null)
        {
            // Destroy(gameObject);
            Instance = this;
        }
    }

    private void Start()
    {
        currentGameState = GameState.Menu;
        _playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        _levelManager = LevelManager.Instance;
        _menuManager = MenuManager.Instance;
    }

    private void Update()
    {
        if (Input.GetButtonDown("Submit") && !currentGameState.Equals(GameState.InGame))
        {
            StartGame();
        }
    }

    public void StartGame()
    {
        SetGameState(GameState.InGame);
    }

    public void GameOver()
    {
        SetGameState(GameState.GameOver);
    }

    public void BackToMenu()
    {
        SetGameState(GameState.Menu);
    }

    private void SetGameState(GameState newGameState)
    {
        switch (newGameState)
        {
            case GameState.Menu:
                _menuManager.HideGameOver();
                _menuManager.HideGameScore();
                _menuManager.ShowMainMenu();
                break;
            case GameState.InGame:
                _levelManager.RemoveAllLevelBlock();
                Invoke("ReloadLevel", 0.1f);
                _menuManager.HideMainMenu();
                _menuManager.HideGameOver();
                _menuManager.ShowGameScore();
                break;
            case GameState.GameOver:
                _menuManager.ShowGameOver();
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(newGameState), newGameState, null);
        }

        currentGameState = newGameState;
    }

    private void ReloadLevel()
    {
        _levelManager.GenerateInitialBlocks();
        _playerController.StartGame();
    }

    public void CollectObject(Collectable collectable)
    {
        collectedObject += collectable.value;
    }
}
