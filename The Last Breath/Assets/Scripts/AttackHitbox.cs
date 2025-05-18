using UnityEngine;

public class AttackHitbox : MonoBehaviour
{
    public float damage = 10f;

    public EfectosSonido soundManager;

    private void Start()
    {
        soundManager = FindFirstObjectByType<EfectosSonido>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<EnemyHealth>(out var enemy))
        {
            enemy.TakeDamage(damage);
            soundManager.SeleccionarSonido(4, 1f);
        }

        // Si es el boss, danarlo tambien
        if (other.TryGetComponent<HealthBoss>(out var boss))
        {
            boss.TakeDamage(damage);
            soundManager.SeleccionarSonido(4, 1f);
        }
    }
}