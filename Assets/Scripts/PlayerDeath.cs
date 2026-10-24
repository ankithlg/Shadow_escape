using UnityEngine;

public class PlayerDeath : MonoBehaviour
{
    public bool isDead { get; private set; } = false;

    public void Die()
    {
        if (isDead) return;

        if (LevelManager.instance == null)
        {
            Debug.LogError("LevelManager instance is null");
            return;
        }

        if (LevelManager.instance.isProcessing) return;

        isDead = true;
        LevelManager.instance.isProcessing = true;

        PlayerMovement movement = GetComponent<PlayerMovement>();
        if (movement != null)
            movement.enabled = false;

        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb != null)
            rb.linearVelocity = Vector2.zero;

        StartCoroutine(DeathAnimation());
    }

    System.Collections.IEnumerator DeathAnimation()
    {
        float duration = 0.4f;
        float elapsed = 0f;
        Vector3 originalScale = transform.localScale;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / duration;
            transform.localScale = Vector3.Lerp(originalScale, Vector3.zero, t);
            yield return null;
        }

        if (LevelManager.instance != null)
            LevelManager.instance.PlayerDied();
        else
            Debug.LogError("LevelManager instance null after death animation");
    }
}