using UnityEngine;

public class AttackerAI : EnemyAI
{
    public float chaseSpeed = 4f;
    private Transform player;
    private bool chasing = false;

    protected override void Start()
    {
        base.Start();
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null)
            player = playerObject.transform;
    }

    void Update()
    {
      chase();
    }

    private void chase()
    {
        if (!chasing || player == null) return;

        if (freeze != null && freeze.isFrozen)
        {
            // HandleFrozenState();
            return;
        }

        PlayerPowers powers = player.GetComponent<PlayerPowers>();
        if (powers != null && powers.IsInvisible())
        {
            chasing = false;
            return;
        }

        transform.position = Vector3.MoveTowards(
            transform.position,
            player.position,
            chaseSpeed * Time.deltaTime
        );
    }
    
    protected override void HandleFrozenState() { }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))
            return;
        
        chasing = true;
        TryKillPlayer(other.transform);
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            chasing = false;
    }
}