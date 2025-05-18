using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class HealthBoss : MonoBehaviour
{
    public float maxHealth = 150f;
    private float currentHealth;

    public GameObject healthBarUI; // El panel completo de la barra
    public Image healthFill;
    public TextMeshProUGUI healthText;
    
    private SpriteRenderer spriteRenderer;
    private Animator animator;

    void Start()
    {
        currentHealth = maxHealth;
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();

        if (healthBarUI != null)
            healthBarUI.SetActive(false); // oculta al inicio
    }

    public void ActivateHealthBar()
    {
        if (healthBarUI != null)
            healthBarUI.SetActive(true);
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        currentHealth = Mathf.Max(0, currentHealth);

        UpdateHealthBar();

        if(spriteRenderer != null)
            StartCoroutine(DamageFlash());

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void UpdateHealthBar()
    {
        if (healthFill != null)
        {
            healthFill.fillAmount = currentHealth / maxHealth;
            healthText.text = Mathf.CeilToInt(currentHealth).ToString();
        }
    }

    void Die()
    {
        var movementScript = GetComponent<BossAngelBehavior>();
        if (movementScript != null)
            movementScript.enabled = false;

        var col = GetComponent<Collider2D>();
        if (col != null)
            col.enabled = false;


        if (animator != null)
            animator.SetTrigger("die"); // Trigger en el Animator

        Destroy(gameObject, 2f); // se elimina despues de animacion
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
