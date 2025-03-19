using System.Collections;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class ScoreUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI current;
    [SerializeField] private TextMeshProUGUI toUpdate;
    [SerializeField] private Transform scoreTextContainer;
    [SerializeField] private float duration = 0.4f;   // Duration of the animation
    [SerializeField] private Ease animationCurve = Ease.OutQuad;  // Animation curve

    private float moveAmount;
    private float containerInitPosition;

    private void Start()
    {
        // Force canvas update to ensure correct UI calculations
        Canvas.ForceUpdateCanvases();

        // Initialize the text displays
        current.SetText("0");
        toUpdate.SetText("0");

        // Get initial container position and move amount from the text height
        containerInitPosition = scoreTextContainer.localPosition.y;
        moveAmount = current.rectTransform.rect.height;

        // Subscribe to the score change event
        ScoreManager.Instance.OnScoreChanged += UpdateScore;

        // Load the score when the scene is loaded
        ScoreManager.Instance.LoadScore();
    }

    private void OnDestroy()
    {
        // Unsubscribe from the score change event when destroyed
        if (ScoreManager.Instance != null)
        {
            ScoreManager.Instance.OnScoreChanged -= UpdateScore;
        }
    }

    // Method to handle score updates
    public void UpdateScore(int score)
    {
        // Update the score display in 'toUpdate' text
        toUpdate.SetText($"{score}");

        // Animate the score container
        scoreTextContainer.DOLocalMoveY(containerInitPosition + moveAmount, duration).SetEase(animationCurve);

        // After the animation is done, reset the position and swap the text
        StartCoroutine(ResetScoreContainer(score));
    }

    // Coroutine to reset the container and swap the text after the animation finishes
    private IEnumerator ResetScoreContainer(int score)
    {
        // Wait for the duration of the animation
        yield return new WaitForSeconds(duration);

        // Set the final score in 'current' text
        current.SetText($"{score}");

        // Reset the container's position
        Vector3 localPosition = scoreTextContainer.localPosition;
        scoreTextContainer.localPosition = new Vector3(localPosition.x, containerInitPosition, localPosition.z);
    }
}
