using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FlappyGameManager : MonoBehaviour
{
    private static FlappyGameManager instance;
    public static FlappyGameManager Instance => instance;

    private FlappyUIManager uIManager;
    public FlappyUIManager UIManager => uIManager;

    private int currentScore = 0;

    private void Awake()
    {
        instance = this;
        uIManager = FindAnyObjectByType<FlappyUIManager>();
    }

    private void Start()
    {
        uIManager.UpdateScore(0);
    }

    public void GameOver()
    {
        uIManager.SetRestart();
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Exit()
    {
        SceneManager.LoadScene("MainScene");
    }

    public void AddScore(int score)
    {
        currentScore += score;
        uIManager.UpdateScore(currentScore);
    }
}
