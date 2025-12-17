using UnityEngine;
using System.Collections;

public class BoxController : MonoBehaviour
{
    public float moveSpeed = 6f;

    [Header("Sensors")]
    public BoxSensor upSensor;
    public BoxSensor downSensor;
    public BoxSensor leftSensor;
    public BoxSensor rightSensor;

    bool isMoving = false;
    Vector2 moveDir;

    void Start()
    {
        transform.position = GridUtils.Snap(transform.position);
    }

    // ?? Player LUÔN ??y ???c box
    public bool TryPush(Vector2 dir)
    {
        if (isMoving) return false;

        moveDir = dir.normalized;
        Vector2 target = GridUtils.Snap((Vector2)transform.position + moveDir);

        StartCoroutine(MoveTo(target));
        return true; // ? C?C QUAN TR?NG
    }

    IEnumerator MoveTo(Vector2 target)
    {
        isMoving = true;

        while (((Vector2)transform.position - target).sqrMagnitude > 0.0001f)
        {
            // ? CH? SENSOR quy?t ??nh d?ng
            if (IsBlocked(moveDir))
                break;

            transform.position = Vector2.MoveTowards(
                transform.position,
                target,
                moveSpeed * Time.deltaTime
            );

            yield return null;
        }

        transform.position = GridUtils.Snap(transform.position);
        isMoving = false;
    }

    bool IsBlocked(Vector2 dir)
    {
        if (dir == Vector2.up) return upSensor.isBlocked;
        if (dir == Vector2.down) return downSensor.isBlocked;
        if (dir == Vector2.left) return leftSensor.isBlocked;
        if (dir == Vector2.right) return rightSensor.isBlocked;
        return false;
    }
}
