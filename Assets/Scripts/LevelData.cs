using UnityEngine;

[System.Serializable]
public class EnemyData
{
    public enum EnemyType { Patrol, Attacker, Guard }
    public EnemyType type;
    public Vector2 position;
    public float patrolDistance = 3f;
    public float guardAngle = 45f;
}

[CreateAssetMenu(menuName = "ShadowEscape/LevelData")]
public class LevelData : ScriptableObject
{
    public string levelName;
    public Vector2 playerStart;
    public Vector2 exitPosition;
    public GameObject wallPrefab;
    public GameObject exitPrefab;
    public EnemyData[] enemies;
}
