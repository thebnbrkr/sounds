using System.Collections;
using UnityEngine;
using TMPro;

public class LivesManager : MonoBehaviour
{
    public int lives = 3;

    public TextMeshProUGUI livesText;

    public GameObject gameOverPanel;

    void Start()
    {
        UpdateLivesUI();
        gameOverPanel.SetActive(false);
    }

    public void LoseLife()
    {
        lives--;
        UpdateLivesUI();

        if (lives <= 0)
        {
            StartCoroutine(GameOverRoutine());
        }
    }

    void UpdateLivesUI()
    {
        livesText.text = "Lives: " + lives;
    }

    IEnumerator GameOverRoutine()
    {
        Time.timeScale = 0;
        gameOverPanel.SetActive(true);
        yield return new WaitForSecondsRealtime(1.5f);
        Time.timeScale = 1;
        SceneHandler.Instance.LoadMenuScene();
    }
}
