using UnityEngine;
using System.Collections;

public class EnemyHealth : MonoBehaviour
{
    public float maxHealth = 50f;
    private float currentHealth;

    private SpriteRenderer spriteRenderer;
    private Animator animator;
    private bool isDying = false;

    void Start()
    {
        currentHealth = maxHealth;
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    public void TakeDamage(float amount)
    {
        if (isDying) return;

        currentHealth -= amount;

        if(spriteRenderer != null)
            StartCoroutine(DamageFlash());

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        isDying = true;

        if (animator != null)
        {
            animator.SetTrigger("die");
        }

        Destroy(gameObject, 0.2f);
    }

    IEnumerator DamageFlash()
    {
        float flashTime = 0.1f;
        int flashCount = 4;

        for (int i = 0; i < flashCount; i++)
        {
            spriteRenderer.enabled = false;
            yield return new WaitForSeconds(flashTime);
            spriteRenderer.enabled = true;
            yield return new WaitForSeconds(flashTime);
        }
    }
}
