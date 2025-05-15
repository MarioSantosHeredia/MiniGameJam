using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    public Transform groundCheck;
    public float groundCheckRadius = 0.1f;
    public LayerMask groundLayer;

    private Rigidbody2D rb;
    private Animator animator;
    private Vector2 movement;
    private bool isGrounded;
    private bool isCrouching;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // Movimiento horizontal (A/D o flechas)
        movement.x = Input.GetAxisRaw("Horizontal");

        // Reproduce animacion si se esta moviendo
        if (animator != null)
        {
            animator.SetBool("isWalking", movement.x != 0 && isGrounded && !isCrouching);
        }

        // Flip visual segun direccion
        if (movement.x < 0)
            transform.localScale = new Vector3(-1, 1, 1);
        else if (movement.x > 0)
            transform.localScale = new Vector3(1, 1, 1);

        // Salto (Space)
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
        animator.SetBool("isJumping", !isGrounded);
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }

        // Agacharse (S)
        if (Input.GetKey(KeyCode.S) && isGrounded)
        {
            isCrouching = true;
        }
        else
        {
            isCrouching = false;
        }
        animator.SetBool("isCrouching", isCrouching);

        // Patada agachada (K agachada)
        if (isCrouching && Input.GetKeyDown(KeyCode.K) && isGrounded)
        {
            animator.SetTrigger("crouchKick");
        }

        // Patada (K)
        if (Input.GetKeyDown(KeyCode.K) && !isCrouching && isGrounded)
        {
            animator.SetTrigger("kick");
        }

        // Patada voladora (K en el aire)
        if (Input.GetKeyDown(KeyCode.K) && !isGrounded)
        {
            animator.SetTrigger("jumpKick");
        }

        // Punetazo (J)
        if (Input.GetKeyDown(KeyCode.J) && !isCrouching && isGrounded)
        {
            animator.SetTrigger("punch");
        }

        // Recibir dano (por ejemplo, tecla H solo para probar)
        if (Input.GetKeyDown(KeyCode.H))
        {
            animator.SetTrigger("hit");
        }
    }

    void FixedUpdate()
    {
        if (!isCrouching)
        {
            rb.linearVelocity = new Vector2(movement.x * moveSpeed, rb.linearVelocity.y);
        }
        else
        {
            rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);
        }
    }
}