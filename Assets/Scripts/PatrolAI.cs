using UnityEngine;

public class PatrolAI : EnemyAI
{
    public float speed = 2f;
    public float patrolDistance = 3f;
    public int spawnIndex = 0;

    private int direction = 1;
    private float currentDistance = 0f;
    private Rigidbody2D rb;

    protected override void Start()
    {
        base.Start();
        rb = GetComponent<Rigidbody2D>();
        direction = (spawnIndex % 2 == 0) ? 1 : -1;
        FaceDirection(direction);
    }
    
    void FixedUpdate()
    {
        if (freeze != null && freeze.isFrozen)
        {
            HandleFrozenState();
            return;
        }
        Move();
    }

    protected override void HandleFrozenState()
    {
        if (rb != null) rb.linearVelocity = Vector2.zero;
    }

    void Move()
    {
        float step = direction * speed * Time.fixedDeltaTime;
        currentDistance += step;

        if (currentDistance >= patrolDistance)
        {
            currentDistance = patrolDistance;
            direction = -1;
            FaceDirection(direction);
        }
        else if (currentDistance <= -patrolDistance)
        {
            currentDistance = -patrolDistance;
            direction = 1;
            FaceDirection(direction);
        }

        if (rb != null)
            rb.linearVelocity = new Vector2(direction * speed, 0f);
        else
            transform.position += Vector3.right * step;
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))
            return;
        
        TryKillPlayer(other.transform);
    }

    void FaceDirection(int dir)
    {
        Vector3 scale = transform.localScale;
        scale.x = Mathf.Abs(scale.x) * dir;
        transform.localScale = new Vector2(-scale.x, scale.y);
    }
}