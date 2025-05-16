using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public int attackDamage = 25;
    public LayerMask enemyLayer;
    public float attackRange = 1f;
    public Transform attackPoint;

    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Se llamara desde un evento en la animacion
    public void PerformAttack()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayer);
        foreach (Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<BossCombatController>()?.TakeDamage(attackDamage);
        }
    }

    void OnDrawGizmosSelected()
    {
        if (attackPoint == null) return;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}