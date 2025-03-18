using UnityEngine;
using TMPro;

public class ScoreUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private string scorePrefix = "Score: ";

    private void Start()
    {
        if (scoreText == null)
        {
            Debug.LogError("ScoreUI: Score Text reference is missing!");
            return;
        }

        // Subscribe to score changes
        ScoreManager.Instance.OnScoreChanged += UpdateScoreDisplay;
        
        // Initialize display
        UpdateScoreDisplay(ScoreManager.Instance.CurrentScore);
    }

    private void OnDestroy()
    {
        if (ScoreManager.Instance != null)
        {
            ScoreManager.Instance.OnScoreChanged -= UpdateScoreDisplay;
        }
    }

    private void UpdateScoreDisplay(int score)
    {
        scoreText.text = $"{scorePrefix}{score}";
    }
} 