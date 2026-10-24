using UnityEngine;

public class GameplayMusicManager : MonoBehaviour
{
    public static GameplayMusicManager instance;

    public AudioSource audioSource;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        float savedTime = GameManager.instance != null ? GameManager.instance.gameplayMusicPosition : 0f;
        audioSource.loop = true;
        audioSource.time = savedTime;
        audioSource.Play();
    }

    public void SavePosition()
    {
        if (GameManager.instance != null)
            GameManager.instance.gameplayMusicPosition = audioSource.time;
    }
}
