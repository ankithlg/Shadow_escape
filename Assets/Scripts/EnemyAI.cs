using UnityEngine;

public abstract class EnemyAI : MonoBehaviour
{
    protected EnemyFreeze freeze;

    protected virtual void Start()
    {
        freeze = GetComponent<EnemyFreeze>();

        if (freeze == null)
            Debug.LogWarning(gameObject.name + " has no EnemyFreeze component.");
    }

    protected bool IsPlayerImmune(Transform playerTransform)
    {
        if (freeze != null && freeze.isFrozen)
            return true;

        PlayerPowers powers = playerTransform.GetComponent<PlayerPowers>();
        if (powers != null && powers.IsInvisible())
            return true;

        return false;
    }

    protected void TryKillPlayer(Transform playerTransform)
    {
        if (IsPlayerImmune(playerTransform))
            return;

        PlayerDeath death = playerTransform.GetComponent<PlayerDeath>();
        if (death != null && !death.isDead)
            death.Die();
    }

    protected abstract void HandleFrozenState();
}