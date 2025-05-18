using UnityEngine;

public class BossController : MonoBehaviour
{
    public Transform player;
    public float startOffsetX = 1.5f;
    public float retreatSpeed = 2f;
    public float retreatDistance = 1.5f;
    public float battleTriggerX = 12.4f;
    public GameObject leftWall;
    public GameObject rightWall;

    private bool isRetreating = true;
    public bool battleStarted = false;

    float originalY;
    float hoverSpeed = 2f;
    float hoverHeight = 0.05f;

    void Start()
    {
        // Guardamos la altura inicial para el efecto de flote
        originalY = transform.position.y;

        // Aparece a X unidades delante del jugador
        transform.position = new Vector3(player.position.x + startOffsetX, transform.position.y, transform.position.z);

        // Las paredes de combate estan ocultas al inicio
        leftWall.SetActive(false);
        rightWall.SetActive(false);
    }

    void Update()
    {
        if (!battleStarted)
        {
            // El efecto de flotar
            float y = originalY + Mathf.Sin(Time.time * hoverSpeed) * hoverHeight;
            
            // Movimiento hacia atras mientras el jugador se acerca
            if (isRetreating && player.position.x >= transform.position.x - retreatDistance)
            {
                transform.position = new Vector3(transform.position.x + retreatSpeed * Time.deltaTime, y, transform.position.z);
            }
            else
            {
                transform.position = new Vector3(transform.position.x, y, transform.position.z);
            }

            // Detectar si ha llegado a la zona de combate
            if (transform.position.x >= battleTriggerX)
            {
                isRetreating = false;
                StartBattle();
            }
        }
    }

    void StartBattle()
    {
        battleStarted = true;

        // Activa las paredes para bloquear al jugador
        leftWall.SetActive(true);
        rightWall.SetActive(true);

        GetComponent<HealthBoss>().ActivateHealthBar();

        CameraFollow camFollow = Camera.main.GetComponent<CameraFollow>();
        camFollow.lockCamera = true;

        camFollow.lockedPosition = new Vector3(10.35f, camFollow.fixedY, -10f);
    }
}