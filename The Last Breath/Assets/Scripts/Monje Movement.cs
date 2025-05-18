using UnityEngine;
using System.Collections;

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

    private EfectosSonido soundManager;

    private float damageCooldown = 1f; // tiempo entre danos
    private float nextDamageTime = 0f;
    private SpriteRenderer spriteRenderer;
    private bool isInvulnerable = false;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        soundManager = FindFirstObjectByType<EfectosSonido>();
        spriteRenderer = GetComponent<SpriteRenderer>();
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
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else if (movement.x > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        // Salto (W o flecha hacia arriba)
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
        animator.SetBool("isJumping", !isGrounded);
        if ((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetButtonDown("Jump"))
            && isGrounded && !isCrouching)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            soundManager.SeleccionarSonido(1, 1f);
        }

        // Agacharse (S o flecha hacia abajo)
        if ((Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)) && isGrounded)
        {
            isCrouching = true;
            soundManager.SeleccionarSonido(2, 0.25f);
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
            soundManager.SeleccionarSonido(3, 0.5f);
        }

        // Patada (K)
        if (Input.GetKeyDown(KeyCode.K) && !isCrouching && isGrounded)
        {
            animator.SetTrigger("kick");
            soundManager.SeleccionarSonido(3, 0.5f);
        }

        // Patada voladora (K en el aire)
        if (Input.GetKeyDown(KeyCode.K) && !isGrounded)
        {
            animator.SetTrigger("jumpKick");
            soundManager.SeleccionarSonido(3, 0.5f);
        }

        // Punetazo (J)
        if (Input.GetKeyDown(KeyCode.J) && !isCrouching && isGrounded)
        {
            animator.SetTrigger("punch");
            soundManager.SeleccionarSonido(3, 0.5f);
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

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(Time.time < nextDamageTime || isInvulnerable) return;

        if (collision.gameObject.CompareTag("Enemigo") || collision.gameObject.CompareTag("Pinchos"))
        {
            // Inicia el cooldown
            nextDamageTime = Time.time + damageCooldown;
            StartCoroutine(FlashDamageEffect(damageCooldown, 0.1f));

            // Activa la animacion de dano
            if (animator != null)
            {
                animator.SetTrigger("hit");
            }
            // Reproducir un sonido o aplicar dano al jugador
            soundManager.SeleccionarSonido(4, 0.5f);

            // Retroceso en la direccion a la que se dirige el enemigo
            Vector2 knockbackDirection = (transform.position.x < collision.transform.position.x) ? Vector2.left : Vector2.right;
            float knockbackForce = 5f; // puedes ajustar la intensidad

            rb.linearVelocity = new Vector2(knockbackDirection.x * knockbackForce, 2f);

            // Aplica el dano correspondiente
            if (collision.gameObject.CompareTag("Pinchos"))
            {
                GetComponent<PlayerHealth>().TakeDamage(100f); // muerte instantanea
            }
            else
            {
                EnemyType enemigo = collision.gameObject.GetComponent<EnemyType>();

                // Dano segun el tipo de enemigo
                GetComponent<PlayerHealth>().TakeDamage(enemigo.damageAmount);
            }
        }    
    }

    IEnumerator FlashDamageEffect(float duration, float flashInterval)
    {
        isInvulnerable = true;
        float time = 0f;

        while (time < duration)
        {
            spriteRenderer.enabled = !spriteRenderer.enabled;
            yield return new WaitForSeconds(flashInterval);
            time += flashInterval;
        }

        spriteRenderer.enabled = true; // asegurate de dejarlo visible
        isInvulnerable = false;
    }
}