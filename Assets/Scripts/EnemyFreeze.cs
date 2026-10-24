using UnityEngine;

public class EnemyFreeze : MonoBehaviour
{
    public bool isFrozen { get; private set; } = false;

    private float freezeTimer = 0f;
    private SpriteRenderer srParent;
    private SpriteRenderer srChild;
    private Color  originalColorparent;
    private Color originalColorchild;

    void Start()
    {
        srParent = GetComponent<SpriteRenderer>();
        if (srParent != null)
        {
            originalColorparent = srParent.color;
        }
        
        srChild = GetComponentInChildren<SpriteRenderer>();
        if (srChild != null)
        {
            originalColorchild = srChild.color;
        }
        
    }

    void Update()
    {
        if (isFrozen)
        {
            freezeTimer -= Time.deltaTime;

            if (freezeTimer <= 0f)
                Unfreeze();
        }
    }

    public void Freeze(float duration)
    {
        isFrozen = true;
        freezeTimer = duration;

        if (srParent != null)
            srParent .color = Color.cyan;
        
        if (srChild != null)
            srChild.color = Color.cyan;
            

        Debug.Log(gameObject.name + " frozen for " + duration + "s");
    }

    void Unfreeze()
    {
        isFrozen = false;
        freezeTimer = 0f;

        if (srParent != null)
            srParent.color =  originalColorparent;
        
        if (srChild != null)
            srChild.color =  originalColorchild;
    }
}