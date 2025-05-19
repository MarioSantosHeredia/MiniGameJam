using UnityEngine;
using System.Collections;

public class NPCController : MonoBehaviour
{
    public float leftLimitX = -5f;
    public float rightLimitX = 5f;
    public float moveSpeed = 2f;
    public float waitTime = 2f;

    private float currentTarget;
    private Animator animator;
    private bool isWaiting = false;

    void Start()
    {
        currentTarget = rightLimitX;
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (isWaiting) return;

        // Movimiento hacia el objetivo
        Vector3 targetPosition = new Vector3(currentTarget, transform.position.y, transform.position.z);
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

        // Cambiar animacion segun movimiento
        if (animator != null)
        {
            bool isWalking = Mathf.Abs(transform.position.x - currentTarget) > 0.01f;
            animator.SetBool("isWalking", isWalking);
        }

        // Mirar en direccion al movimiento
        if(currentTarget - transform.position.x != 0)
            transform.localScale = new Vector3(Mathf.Sign(currentTarget - transform.position.x), 1, 1);

        // Al llegar al punto
        if (Mathf.Abs(transform.position.x - currentTarget) < 0.01f)
        {
            StartCoroutine(WaitAndSwitch());
        }
    }

    IEnumerator WaitAndSwitch()
    {
        isWaiting = true;
        yield return new WaitForSeconds(waitTime);

        currentTarget = (Mathf.Approximately(currentTarget, leftLimitX)) ? rightLimitX : leftLimitX;
        isWaiting = false;
    }
}
