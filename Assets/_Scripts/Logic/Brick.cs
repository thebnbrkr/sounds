/*

using System.Collections;
using UnityEngine;

public class Brick : MonoBehaviour
{
    private Coroutine destroyRoutine = null;

    private void OnCollisionEnter(Collision other)
    {
        if (destroyRoutine != null) return;
        if (!other.gameObject.CompareTag("Ball")) return;
        destroyRoutine = StartCoroutine(DestroyWithDelay());
    }

    private IEnumerator DestroyWithDelay()
    {
        yield return new WaitForSeconds(0.1f); // two physics frames to ensure proper collision
        GameManager.Instance.OnBrickDestroyed(transform.position);
        Destroy(gameObject);
    }
}

*/

using System.Collections;
using UnityEngine;

public class Brick : MonoBehaviour
{
    [SerializeField] private AudioClip breakSound; // Reference to your sound effect
    private Coroutine destroyRoutine = null;
    
    private void OnCollisionEnter(Collision other)
    {
        if (destroyRoutine != null) return;
        if (!other.gameObject.CompareTag("Ball")) return;
        
        // Play the sound effect
        if (breakSound != null)
        {
            AudioSource.PlayClipAtPoint(breakSound, transform.position);
        }
        
        destroyRoutine = StartCoroutine(DestroyWithDelay());
    }
    
    private IEnumerator DestroyWithDelay()
    {
        yield return new WaitForSeconds(0.1f); // two physics frames to ensure proper collision
        GameManager.Instance.OnBrickDestroyed(transform.position);
        Destroy(gameObject);
    }
}