using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivesManager : MonoBehaviour
{
    public int lives = 3;

    public GameObject gameOverPanel;

    public GameObject heartPrefab;

    public Transform heartsContainer;

    private List<GameObject> heartIcons = new List<GameObject>();

    void Start()
    {
        SetupHearts();
        gameOverPanel.SetActive(false);
    }

    void SetupHearts()
    {
        foreach (Transform child in heartsContainer)
        {
            Destroy(child.gameObject);
        }
        heartIcons.Clear();

        for (int i = 0; i < lives; i++)
        {
            GameObject heart = Instantiate(heartPrefab, heartsContainer);
            heartIcons.Add(heart);
        }
    }

    public void LoseLife()
    {
        lives--;
    
        if (heartIcons.Count > 0)
        {
            GameObject heartToRemove = heartIcons[heartIcons.Count - 1];
    heartIcons.RemoveAt(heartIcons.Count - 1);
            Destroy(heartToRemove);
}

if (lives <= 0)
{
    StartCoroutine(GameOverRoutine());
}
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
