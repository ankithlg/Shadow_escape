using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    
    [Header("Config")]
    [SerializeField] float moveSpeed = 5f;
    
    private Rigidbody2D rb;
    private Vector2 moveInput;
    [SerializeField] private float  jumpForce = 10f;
    private bool canJump = true;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.freezeRotation = true;
    }
    void Update()
    {
        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.y = Input.GetAxisRaw("Vertical");
        moveInput = moveInput.normalized;
    
    }

    void FixedUpdate()
    {
        Move();
    }
    
    void Move()
    {
        Vector2 targetVelocity = moveInput * moveSpeed;
        rb.linearVelocity = targetVelocity;
    }

    // private void Jump()
    // {
    //     if (Input.GetKeyDown(KeyCode.Space) && canJump)
    //     {
    //         Vector2 velocity = rb.linearVelocity;
    //         velocity.y = 0;
    //         rb.linearVelocity = velocity;
    //         
    //         rb.AddForce (2500 * Time.deltaTime * Vector2.up, ForceMode2D.Impulse);
    //         canJump = false;
    //     }
    // }
    
}