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

    void TryMove(Vector2 dir)
    {
        Vector2 start = transform.position;
        Vector2 target = start + dir;

        RaycastHit2D hit = Physics2D.Raycast(start, dir, 1f, blockingLayer);

        // N?u không có v?t c?n ? ?i luôn
        if (hit.collider == null)
        {
            StartCoroutine(MoveTo(target));
            return;
        }

        // N?u g?p h?p ? th? ??y
        if (hit.collider.CompareTag("Box"))
        {
            BoxController box = hit.collider.GetComponent<BoxController>();
            if (box.TryPush(dir))
                StartCoroutine(MoveTo(target));
        }
    }

    IEnumerator MoveTo(Vector2 target)
    {
        isMoving = true;

        animator.SetBool("isRunning", true);   // ?? b?t animation

        while ((Vector2)transform.position != target)
        {
            transform.position = Vector2.MoveTowards(
                transform.position,
                target,
                moveSpeed * Time.deltaTime
            );
            yield return null;
        }

        animator.SetBool("isRunning", false);  // ?? t?t animation
        isMoving = false;
    }
}
