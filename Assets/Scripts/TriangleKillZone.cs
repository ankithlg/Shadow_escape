using UnityEngine;

public class TriangleKillZone : EnemyAI
{
    protected override void Start()
    {
        freeze = GetComponentInParent<EnemyFreeze>();
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))
            return;
        
        TryKillPlayer(other.transform);
    }
    
    protected override void HandleFrozenState()
    {
        
    }
}