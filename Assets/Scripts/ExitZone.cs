using UnityEngine;

public class ExitZone : MonoBehaviour
{
    private bool triggered = false;

    void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log("Exit triggered by: " + col.gameObject.name + " tag: " + col.tag);

        if (triggered) return;

        if (col.CompareTag("Player"))
        {
            triggered = true;
            LevelManager.instance.LevelComplete();
        }
    }
}