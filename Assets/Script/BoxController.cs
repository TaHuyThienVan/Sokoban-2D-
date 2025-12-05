using UnityEngine;
using System.Collections.Generic;
public class BoxController : MonoBehaviour
{
    public float moveSpeed = 6f;
    public LayerMask blockingLayer;   // Wall + Box
    bool isMoving = false;

    public bool TryPush(Vector2 dir)
    {
        if (isMoving) return false;

        Vector2 start = transform.position;
        Vector2 target = start + dir;

        if (Physics2D.Raycast(start, dir, 1f, blockingLayer))
            return false;


        StartCoroutine(MoveTo(target));
        return true;
    }

    System.Collections.IEnumerator MoveTo(Vector2 target)
    {
        isMoving = true;
        while ((Vector2)transform.position != target)
        {
            transform.position = Vector2.MoveTowards(transform.position, target, moveSpeed * Time.deltaTime);
            yield return null;
        }
        isMoving = false;
    }
}
