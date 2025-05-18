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
    public GameObject rightWall;
    
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
        // Para el movimiento
        var movementScript = GetComponent<BossAngelBehavior>();
        if (movementScript != null)
            movementScript.enabled = false;

        // Desactiva el collider
        var col = GetComponent<Collider2D>();
        if (col != null)
            col.enabled = false;

        // Animacion de muerte
        if (animator != null)
            animator.SetTrigger("die"); // Trigger en el Animator
        
        // Desactivar barra de vida
        if (healthBarUI != null)
            healthBarUI.SetActive(false);

        // Volver a activar seguimiento de camara
        CameraFollow cam = Camera.main.GetComponent<CameraFollow>();
        if (cam != null)
            cam.lockCamera = false;

        // Desactivar limite derecho
        if (rightWall != null)
            rightWall.SetActive(false);
        
        // Se elimina el objeto
        Destroy(gameObject, 1f); 
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
