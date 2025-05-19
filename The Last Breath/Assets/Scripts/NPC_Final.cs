using UnityEngine;

public class EndGameProximityTrigger : MonoBehaviour
{
    public GameObject player; // Asigna el Player en el Inspector
    public GameObject dialog; // Tu bocadillo canvas
    public GameObject creditsUI; // El panel de creditos
    public float triggerDistance = 2f;
    public float dialogDuration = 3f;

    private bool triggered = false;

    void Update()
    {
        if (triggered || player == null) return;

        float distance = Vector2.Distance(transform.position, player.transform.position);

        if (distance <= triggerDistance)
        {
            triggered = true;
            StartCoroutine(PlayEndSequence());
        }
    }

    System.Collections.IEnumerator PlayEndSequence()
    {
        // Desactivar movimiento
        var movement = player.GetComponent<PlayerMovement>();
        if (movement != null)
            movement.enabled = false;
        // Activar el dialogo
        if (dialog != null)
            dialog.SetActive(true);

        yield return new WaitForSeconds(dialogDuration);

        if (dialog != null)
            dialog.SetActive(false);

        if (movement != null)
            movement.enabled = true;

        if (creditsUI != null)
            creditsUI.SetActive(true);
    }
}
