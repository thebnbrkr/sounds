using UnityEngine;

public class KillZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Ball"))
        {
            // Find the LivesManager in the scene and call LoseLife()
            LivesManager livesManager = FindObjectOfType<LivesManager>();
            if (livesManager != null)
            {
                livesManager.LoseLife();
            }
            else
            {
                Debug.LogWarning("LivesManager not found in scene.");
            }
        }
    }
}
