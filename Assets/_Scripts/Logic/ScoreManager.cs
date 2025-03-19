using UnityEngine;
using System;

public class ScoreManager : SingletonMonoBehavior<ScoreManager>
{
    public event Action<int> OnScoreChanged;
    private int currentScore = 0;
    private bool isNewGame = true;
    public int CurrentScore => currentScore;

    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(gameObject);
    }

    public void StartNewGame()
    {
        isNewGame = true;
        ResetScore();
    }

    public void AddScore(int points)
    {
        currentScore += points;
        OnScoreChanged?.Invoke(currentScore);
    }

    public void ResetScore()
    {
        currentScore = 0;
        OnScoreChanged?.Invoke(currentScore);

        if (isNewGame)
        {
            PlayerPrefs.DeleteKey("CurrentScore");
            isNewGame = false;
        }
    }
    
    public void SaveScore()
    {
        PlayerPrefs.SetInt("CurrentScore", currentScore);
        PlayerPrefs.Save();  
    }

   
    public void LoadScore()
    {
        // Only load score from PlayerPrefs if it’s not a new game
        if (!isNewGame)
        {
            currentScore = PlayerPrefs.GetInt("CurrentScore", 0);  
            OnScoreChanged?.Invoke(currentScore);  
        }
    }
}