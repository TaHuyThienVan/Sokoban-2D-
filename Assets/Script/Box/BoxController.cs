using UnityEngine;
using System.Collections;

public class BoxController : MonoBehaviour
{
    public float moveSpeed = 6f;
    public LayerMask blockingLayer;

    bool isMoving = false;

    void Start()
    {
        // Ép box vào ?úng tâm ô khi spawn
        transform.position = GridUtils.Snap(transform.position);
    }

    public bool TryPush(Vector2 dir)
    {
        if (isMoving) return false;

        Vector2 start = GridUtils.Snap(transform.position);
        Vector2 target = start + dir;

        // Ki?m tra v?t c?n phía tr??c
        RaycastHit2D hit = Physics2D.Raycast(start, dir, 1f, blockingLayer);
        if (hit.collider != null)
            return false;

        StartCoroutine(MoveTo(target));
        return true;
    }

    IEnumerator MoveTo(Vector2 target)
    {
        isMoving = true;

        target = GridUtils.Snap(target);

        // Dùng sqrMagnitude ?? tránh l?i float l?ch
        while (((Vector2)transform.position - target).sqrMagnitude > 0.0001f)
        {
            transform.position = Vector2.MoveTowards(
                transform.position,
                target,
                moveSpeed * Time.deltaTime
            );
            yield return null;
        }

        transform.position = target;
        isMoving = false;
    }
}
