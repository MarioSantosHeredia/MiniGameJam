using UnityEngine;
using UnityEngine.UI;

public class BossCombatController : MonoBehaviour
{
    public Transform player;
    public float moveSpeed = 2f;
    public float attackCooldown = 3f;
    public float attackRange = 8f;
    public Animator animator;
    public int maxHealth = 100;
    public Slider bossHealthSlider;

    private int currentHealth;
    private float nextAttackTime;
    private bool isAlive = true;

    void Start()
    {
        currentHealth = maxHealth;
        bossHealthSlider.maxValue = maxHealth;
        bossHealthSlider.value = maxHealth;
    }

    void Update()
    {
        if (!isAlive) return;

        // Movimiento horizontal dentro del escenario
        if (Mathf.Abs(player.position.x - transform.position.x) > attackRange / 2f)
        {
            float direction = player.position.x < transform.position.x ? -1 : 1;
            transform.position += new Vector3(direction * moveSpeed * Time.deltaTime, 0f, 0f);
        }

        // Ataque cada cierto tiempo
        if (Time.time >= nextAttackTime)
        {
            animator.SetTrigger("attack");
            nextAttackTime = Time.time + attackCooldown;
        }
    }

    public void TakeDamage(int amount)
    {
        if (!isAlive) return;

        currentHealth -= amount;
        bossHealthSlider.value = currentHealth;

        if (currentHealth <= 0)
        {
            isAlive = false;
            animator.SetTrigger("die");
        }
    }

    // Se llama al final de la animación de muerte (usar Animation Event)
    public void OnDeathAnimationEnd()
    {
        Destroy(gameObject);
    }
}