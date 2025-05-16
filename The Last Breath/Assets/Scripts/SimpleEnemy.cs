using UnityEngine;

public class SimpleEnemy : MonoBehaviour
{
    public float speed = 2f;
    public Transform leftLimit;
    public Transform rightLimit;

    public bool movingRight = true;

    void Update()
    {
        // Movimiento automatico
        if (movingRight)
        {
            transform.position += Vector3.right * speed * Time.deltaTime;
            if (transform.position.x >= rightLimit.position.x)
                movingRight = false;

            transform.localScale = new Vector3(-1, 1, 1);
        }
        else
        {
            transform.position += Vector3.left * speed * Time.deltaTime;
            if (transform.position.x <= leftLimit.position.x)
                movingRight = true;
            
            transform.localScale = new Vector3(1, 1, 1);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Dano al jugador por contacto
        if (other.CompareTag("Player"))
        {
            Debug.Log("Jugador danado");
            // Aqui pododemos llamar a: other.GetComponent<PlayerHealth>().TakeDamage();
        }

        // Morir si recibe cualquier ataque del jugador
        if (other.CompareTag("PlayerAttack"))
        {
            Debug.Log("Enemigo destruido");
            Destroy(gameObject);
        }
    }
}
