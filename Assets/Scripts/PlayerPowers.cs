using UnityEngine;

public class PlayerPowers : MonoBehaviour
{
    [Header("Invisibility Settings")]
    public float invisDuration = 3f;
    public float invisCooldown = 10f;

    [Header("Freeze Settings")]
    public float freezeRadius = 3f;
    public float freezeDuration = 3f;
    public float freezeCooldown = 12f;

    private PlayerMovement movement;
    private SpriteRenderer sr;

    private bool invisActive = false;
    private bool freezeActive = false;
   
    private float freezeTimer;
    private float invisTimer;
    
    private float invisCooldownTimer;
    private float freezeCooldownTimer;

    void Start()
    {
        movement = GetComponent<PlayerMovement>();
        sr = GetComponent<SpriteRenderer>();

        if (movement == null)
            Debug.LogError("PlayerMovement not found on player");

        if (sr == null)
            Debug.LogError("SpriteRenderer not found on player");
    }

    void Update()
    {
        HandleInput();
        HandleCooldowns();
        HandleInvis();
        HandleFreezeEffect();
    }

    void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            if (invisCooldownTimer <= 0f && !invisActive)
            {
                invisActive = true;
                invisTimer = invisDuration;
                
            }
        }
        
        if (Input.GetKeyDown(KeyCode.C))
        {
            if (freezeCooldownTimer <= 0f && !freezeActive)
            {
                FreezeNearbyEnemies();
                freezeActive = true;
                freezeTimer = freezeDuration; 
            }
       
        }
    }
    void HandleInvis()
    {
        if (!invisActive)
            return;

        if (sr != null)
            sr.color = new Color(1f, 1f, 1f, 0.3f);

        invisTimer -= Time.deltaTime;

        if (invisTimer <= 0f)
        {
            invisActive = false;

            if (sr != null)
                sr.color = new Color(1f, 1f, 1f, 1f);
            
            invisCooldownTimer = invisCooldown;
        }
    }

    void HandleCooldowns()
    {
        if (invisCooldownTimer > 0f)
            invisCooldownTimer -= Time.deltaTime;

        if (freezeCooldownTimer > 0f)
            freezeCooldownTimer -= Time.deltaTime;
    }

    void FreezeNearbyEnemies()
    {
        EnemyFreeze[] allEnemies = FindObjectsByType<EnemyFreeze>(FindObjectsSortMode.None);
        int frozenCount = 0;

        foreach (EnemyFreeze freeze in allEnemies)
        {
            float dist = Vector2.Distance(transform.position, freeze.transform.position);
            if (dist <= freezeRadius)
            {
                freeze.Freeze(freezeDuration);
                frozenCount++;
            }
        }

        Debug.Log("Freeze hit " + frozenCount + " enemies");
    }
    
    void HandleFreezeEffect()
    {
        if (!freezeActive)
            return;

        freezeTimer -= Time.deltaTime;

        if (freezeTimer <= 0f)
        {
            freezeActive = false;
            freezeCooldownTimer = freezeCooldown;   // starts at 12s
        }
    }

    public bool IsInvisible() => invisActive;
    public float GetInvisCooldown() => Mathf.Max(0f, invisCooldownTimer);
    public float GetFreezeCooldown() => Mathf.Max(0f, freezeCooldownTimer);
    public bool IsFreezeActive() => freezeActive;
    
}