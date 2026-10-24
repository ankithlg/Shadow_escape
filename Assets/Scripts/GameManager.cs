using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("Game State")]
    public int totalLives = 3;
    public int currentLevelIndex = 0;
    [HideInInspector] public float gameplayMusicPosition = 0f;

    [Header("Prefabs")]
    public GameObject playerPrefab;
    public GameObject patrolPrefab;
    public GameObject attackerPrefab;
    public GameObject guardPrefab;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void ResetGame()
    {
        totalLives = 3;
        currentLevelIndex = 0;
        gameplayMusicPosition = 0f;
    }
}
