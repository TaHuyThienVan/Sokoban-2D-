using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 6f;
    public LayerMask blockingLayer;
    bool isMoving = false;

    [SerializeField] private Animator animator;

    void Update()
    {
        if (isMoving) return;

        Vector2 dir = Vector2.zero;

        if (Input.GetKeyDown(KeyCode.W)) dir = Vector2.up;
        if (Input.GetKeyDown(KeyCode.S)) dir = Vector2.down;
        if (Input.GetKeyDown(KeyCode.A)) dir = Vector2.left;
        if (Input.GetKeyDown(KeyCode.D)) dir = Vector2.right;

        if (dir != Vector2.zero)
            TryMove(dir);
    }

    void TryMove(Vector2 direction)
    {
        Vector2 start = transform.position;
        Vector2 target = start + direction;

        RaycastHit2D hit = Physics2D.Raycast(start, direction, 1f, blockingLayer);

        if (hit.collider == null)
        {
            animator.SetBool("isRunning", true);
            StartCoroutine(MoveTo(target));
            return;
        }

        if (hit.collider.CompareTag("Box"))
        {
            BoxController box = hit.collider.GetComponent<BoxController>();

            if (box.TryPush(direction))  
            {
                animator.SetTrigger("Push");   
                animator.SetBool("isRunning", false); 
                StartCoroutine(MoveTo(target));
            }
            else
            {
          
                animator.SetBool("isRunning", false);
            }
        }
    }

    IEnumerator MoveTo(Vector2 target)
    {
        isMoving = true;

        while ((Vector2)transform.position != target)
        {
            transform.position = Vector2.MoveTowards(
                                                            transform.position,
                                                            target,
                                                            moveSpeed * Time.deltaTime
                                                        );

            yield return null;
        }

        animator.SetBool("isRunning", false); 
        isMoving = false;
    }
}
