using System;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public Canvas menuCanvas;
    public Canvas gameOverCanvas;
    public Canvas gameCanvas;
    
    public static MenuManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    private void Start()
    {
        
    }

    private void Update()
    {
        
    }
    
    public void ShowMainMenu()
    {
        menuCanvas.enabled = true;
    }
    
    public void ShowGameOver()
    {
        gameOverCanvas.enabled = true;
    }
    
    public void ShowGameScore()
    {
        gameCanvas.enabled = true;
    }
    
    public void HideMainMenu()
    {
        menuCanvas.enabled = false;
    }
    
    public void HideGameOver()
    {
        gameOverCanvas.enabled = false;
    }
    
    public void HideGameScore()
    {
        gameCanvas.enabled = false;
    }
    
    public void ExitGame()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
}
