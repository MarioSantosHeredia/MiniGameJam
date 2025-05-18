using UnityEngine;

public class EnemyType : MonoBehaviour
{
    public enum TipoEnemigo { Angel, Enemy1}
    public TipoEnemigo tipo;
    public float damageAmount = 10f;

    public void TakeDamage(float amount)
    {
        // Aqui puedes poner animaciones, efectos, o muerte
        Debug.Log(gameObject.name + " recibio " + amount + " de dano.");
        Destroy(gameObject); // o reduce vida si tienes sistema de salud
    }
}
