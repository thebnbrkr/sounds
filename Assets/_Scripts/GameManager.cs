using UnityEngine;

public class GameManager : SingletonMonoBehavior<GameManager>
{
    [SerializeField] private int maxLives = 3;
    [SerializeField] private Ball ball;
    [SerializeField] private Transform bricksContainer;
    [SerializeField] private int pointsPerBrick = 10;

    private int currentBrickCount;
    private int totalBrickCount;

    private void OnEnable()
    {
        if (ScoreManager.Instance == null)
        {
            // Debug.Log("Initializing ScoreManager manually...");
            GameObject scoreManagerObject = new GameObject("ScoreManager");
            scoreManagerObject.AddComponent<ScoreManager>();
        }
        InputHandler.Instance.OnFire.AddListener(FireBall);
        ball.ResetBall();
        totalBrickCount = bricksContainer.childCount;
        currentBrickCount = bricksContainer.childCount;
        
       if (ScoreManager.Instance.CurrentScore == 0)
        {
            ScoreManager.Instance.StartNewGame();  // Start fresh game
        }
        // Load the score when the game starts
        ScoreManager.Instance.LoadScore();
    }

    private void OnDisable()
    {
        InputHandler.Instance.OnFire.RemoveListener(FireBall);
    }

    private void FireBall()
    {
        ball.FireBall();
    }

    public void OnBrickDestroyed(Vector3 position)
    {
        // fire audio here
        // implement particle effect here
        CameraShake.Shake(0.6F, 0.05F);
        currentBrickCount--;
        ScoreManager.Instance.AddScore(pointsPerBrick);
        Debug.Log($"Destroyed Brick at {position}, {currentBrickCount}/{totalBrickCount} remaining");

        if(currentBrickCount == 0)
        {
            // Save the score before loading the next scene
            ScoreManager.Instance.SaveScore();

            // Load the next scene
            SceneHandler.Instance.LoadNextScene();
        }
    }

    public void KillBall()
    {
        maxLives--;
        // update lives on HUD here
        // game over UI if maxLives < 0, then exit to main menu after delay
        ball.ResetBall();

        if (maxLives < 0)
        {
            // Reset the score
            ScoreManager.Instance.ResetScore();

            // Optionally, load the game over scene or main menu here
            // SceneHandler.Instance.LoadGameOverScene();
        }
    }
}
