using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth = 100f;
    public float currentHealth;
    public float regenRate = 1f; // puntos por segundo
    public float regenDelay = 3f; // segundos tras recibir dano

    public Image HealthBar;
    public TextMeshProUGUI HealthText;

    private float lastDamageTime;

    void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthBar();
    }

    void Update()
    {
        // Regenerar si ha pasado suficiente tiempo desde el ultimo dano
        if (Time.time - lastDamageTime > regenDelay && currentHealth < maxHealth)
        {
            currentHealth += regenRate * Time.deltaTime;
            currentHealth = Mathf.Min(currentHealth, maxHealth);
            UpdateHealthBar();
        }
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        currentHealth = Mathf.Max(0, currentHealth);
        lastDamageTime = Time.time;

        UpdateHealthBar();

        if (currentHealth <= 0)
        {
            gameObject.SetActive(false);
            Invoke(nameof(RestartLevel), 2f);
        }
    }

    void UpdateHealthBar()
    {
        if (HealthBar != null)
        {
            HealthBar.fillAmount = currentHealth / maxHealth;
            HealthText.text = Mathf.CeilToInt(currentHealth).ToString();
        }
    }

    void RestartLevel()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.buildIndex);
    }
}
