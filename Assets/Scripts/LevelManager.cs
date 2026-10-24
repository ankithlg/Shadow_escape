using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;
    public bool isProcessing = false;

    [Header("Levels")]
    public LevelData[] levels;

    void Awake()
    { 
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "GameScene")
        {
            isProcessing = false;

            if (GameManager.instance == null)
            {
                Debug.LogError("GameManager not found — make sure it is in GameScene");
                return;
            }

            LoadLevel(GameManager.instance.currentLevelIndex);
        }
    }

    void Start()
    {
        isProcessing = false;
        if (SceneManager.GetActiveScene().name == "GameScene")
            LoadLevel(GameManager.instance.currentLevelIndex);
    }

    public void LoadLevel(int index)
    {
        if (GameManager.instance == null)
        {
            Debug.LogError("GameManager instance is null");
            return;
        }

        if (index < 0 || index >= levels.Length)
        {
            Debug.LogError("Level index out of range: " + index);
            return;
        }

        Debug.Log("Loading level: " + index);
        isProcessing = false;
        ClearScene();

        LevelData level = levels[index];
        SpawnWalls(level);
        SpawnEnemies(level);
        SpawnPlayer(level);
        SpawnExit(level);
    }

    void ClearScene()
    {
        foreach (var g in GameObject.FindGameObjectsWithTag("Wall"))
            if (g != null) Destroy(g);

        foreach (var g in GameObject.FindGameObjectsWithTag("Enemy"))
            if (g != null) Destroy(g);

        foreach (var g in GameObject.FindGameObjectsWithTag("Exit"))
            if (g != null) Destroy(g);

        foreach (var g in GameObject.FindGameObjectsWithTag("Player"))
            if (g != null) Destroy(g);
    }

    void SpawnWalls(LevelData level)
    {
        if (level.wallPrefab == null)
        {
            Debug.LogError("Wall prefab null for " + level.levelName);
            return;
        }
        GameObject wall = Instantiate(level.wallPrefab, Vector3.zero, Quaternion.identity);
        wall.tag = "Wall";
    }

    void SpawnEnemies(LevelData level)
    {
        int patrolIndex = 0;
        foreach (var e in level.enemies)
        {
            GameObject prefab = e.type == EnemyData.EnemyType.Patrol
                              ? GameManager.instance.patrolPrefab
                              : e.type == EnemyData.EnemyType.Attacker
                              ? GameManager.instance.attackerPrefab
                              : GameManager.instance.guardPrefab;

            if (prefab == null)
            {
                Debug.LogError("Enemy prefab null for type: " + e.type);
                continue;
            }

            GameObject enemy = Instantiate(prefab, e.position, Quaternion.identity);
            enemy.tag = "Enemy";

            if (e.type == EnemyData.EnemyType.Patrol)
            {
                PatrolAI patrol = enemy.GetComponent<PatrolAI>();
                if (patrol != null)
                {
                    patrol.patrolDistance = e.patrolDistance;
                    patrol.spawnIndex = patrolIndex;
                }
                patrolIndex++;
            }

            // if (e.type == EnemyData.EnemyType.Guard)
            // {
            //     VisionCone cone = enemy.GetComponent<VisionCone>();
            //     if (cone != null)
            //         cone.angle = e.guardAngle;
            // }
        }
    }

    void SpawnPlayer(LevelData level)
    {
        if (GameManager.instance.playerPrefab == null)
        {
            Debug.LogError("Player prefab is null");
            return;
        }
        GameObject player = Instantiate(
            GameManager.instance.playerPrefab,
            level.playerStart,
            Quaternion.identity);
        player.tag = "Player";
    }

    void SpawnExit(LevelData level)
    {
        if (level.exitPrefab == null)
        {
            Debug.LogError("Exit prefab null for " + level.levelName);
            return;
        }
        GameObject exit = Instantiate(
            level.exitPrefab,
            level.exitPosition,
            Quaternion.identity);
        exit.tag = "Exit";
    }

    public void PlayerDied()
    {
        if (GameplayMusicManager.instance != null)
            GameplayMusicManager.instance.SavePosition();

        GameManager.instance.totalLives--;

        if (GameManager.instance.totalLives <= 0)
        {
            GoToResult(
                title:   "Game Over",
                message: "You have no lives left. Better luck next time!",
                button:  "Play Again",
                isOver:  true
            );
        }
        else
        {
            GoToResult(
                title:   "You Died!",
                message: $"You have {GameManager.instance.totalLives} " +
                         $"{(GameManager.instance.totalLives == 1 ? "life" : "lives")} remaining.",
                button:  "Try Again",
                isOver:  false
            );
        }
    }

    public void LevelComplete()
    {
        if (isProcessing) return;
        isProcessing = true;

        if (GameplayMusicManager.instance != null)
            GameplayMusicManager.instance.SavePosition();

        int nextIndex = GameManager.instance.currentLevelIndex + 1;

        if (nextIndex >= levels.Length)
        {
            GameManager.instance.ResetGame();
            GoToResult(
                title:   "You Won!",
                message: "You completed all levels. Master of shadows!",
                button:  "Play Again",
                isOver:  false
            );
        }
        else
        {
            GameManager.instance.currentLevelIndex = nextIndex;
            GoToResult(
                title:   "Level Clear!",
                message: $"Well done! Get ready for Level {nextIndex + 1}.",
                button:  "Next Level",
                isOver:  false
            );
        }
    }

    void GoToResult(string title, string message, string button, bool isOver)
    {
        ResultData.instance.titleText   = title;
        ResultData.instance.messageText = message;
        ResultData.instance.buttonText  = button;
        ResultData.instance.isGameOver  = isOver;
        SceneManager.LoadScene("ResultScene");
    }
}
