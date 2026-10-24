using UnityEngine;

public class GuardAI : MonoBehaviour
{
    [Header("Rotation")]
    public float rotateSpeed = 60f;

    private Transform triangle;

    void Start()
    {

        triangle = transform.Find("Triangle");

        if (triangle == null)
        {
            Debug.LogError("Triangle child not found on " + gameObject.name);
        }
    }

    void Update()
    {
        Rotate();
    }

    private void Rotate()
    {
        EnemyFreeze freeze = GetComponent<EnemyFreeze>();

        if (freeze != null && freeze.isFrozen)
            return;

        if (triangle != null)
        {
            triangle.RotateAround(
                transform.position,
                Vector3.forward,
                rotateSpeed * Time.deltaTime
            );
            
        }
    }
    
    
}
