using UnityEngine;
using System;

public class ScoreManager : SingletonMonoBehavior<ScoreManager>
{
    public event Action<int> OnScoreChanged;

    private int currentScore = 0;

    public int CurrentScore => currentScore;

    public void AddScore(int points)
    {
        currentScore += points;
        OnScoreChanged?.Invoke(currentScore);
    }

    public void ResetScore()
    {
        currentScore = 0;
        OnScoreChanged?.Invoke(currentScore);
    }
}