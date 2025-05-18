using UnityEngine;

public class AttackHitbox : MonoBehaviour
{
    public float damage = 10f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<EnemyHealth>(out var enemy))
        {
            enemy.TakeDamage(damage);
        }

        // Si es el boss, danarlo tambien
        if (other.TryGetComponent<HealthBoss>(out var boss))
        {
            boss.TakeDamage(damage);
        }
    }
}