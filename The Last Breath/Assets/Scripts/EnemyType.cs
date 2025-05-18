using UnityEngine;

public class EnemyType : MonoBehaviour
{
    public enum TipoEnemigo { Angel, Enemy1}
    public TipoEnemigo tipo;
    public float damageAmount = 10f;
}
