using UnityEngine;

public class BossAngelBehavior : MonoBehaviour
{
    public float patrolSpeed = 2f;
    public float attackInterval = 4f;
    public float attackSpeed = 8f;
    public float hoverY = 3f;

    public float leftLimit = 8f;
    public float rightLimit = 13f;

    public Transform player;

    private Animator animator;
    private bool movingRight = true;
    private bool isAttacking = false;
    private float attackTimer = 0f;

    void Start()
    {
        animator = GetComponent<Animator>();
        transform.position = new Vector3(transform.position.x, hoverY, transform.position.z);
    }

    void Update()
    {
        if (isAttacking)
        {
            Vector3 targetPos = new Vector3(transform.position.x, player.position.y, transform.position.z);
            transform.position = Vector3.MoveTowards(transform.position, targetPos, attackSpeed * Time.deltaTime);

            if (Mathf.Abs(transform.position.y - player.position.y) < 0.1f)
            {
                Invoke("ReturnToHover", 0.5f);
                isAttacking = false;
            }

            return;
        }

        // Movimiento horizontal
        float moveDir = movingRight ? 1f : -1f;
        transform.Translate(Vector2.right * moveDir * patrolSpeed * Time.deltaTime);

        // Voltear sprite
        transform.localScale = new Vector3(moveDir, 1f, 1f);

        // Cambiar dirección al alcanzar los límites
        if (transform.position.x >= rightLimit)
            movingRight = false;
        else if (transform.position.x <= leftLimit)
            movingRight = true;

        // Temporizador de ataque
        attackTimer += Time.deltaTime;
        if (attackTimer >= attackInterval)
        {
            attackTimer = 0f;
            StartAttack();
        }
    }

    void StartAttack()
    {
        isAttacking = true;
        if (animator != null)
        {
            animator.SetTrigger("attack");
        }
    }

    void ReturnToHover()
    {
        StartCoroutine(RiseBackUp());
    }

    System.Collections.IEnumerator RiseBackUp()
    {
        Vector3 targetPos = new Vector3(transform.position.x, hoverY, transform.position.z);

        while (Mathf.Abs(transform.position.y - hoverY) > 0.05f)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, attackSpeed * Time.deltaTime);
            yield return null;
        }

        transform.position = new Vector3(transform.position.x, hoverY, transform.position.z);
        isAttacking = false;
    }
}
