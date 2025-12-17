using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 6f;
    public LayerMask blockingLayer;
    public bool isFacingRight = true;

    bool isMoving = false;
    [SerializeField] private Animator animator;

    void Start()
    {
        transform.position = GridUtils.Snap(transform.position);
    }

    void Update()
    {
        HandleMoving();
    }

    void HandleMoving()
    {
        if (isMoving) return;

        Vector2 dir = Vector2.zero;

        if (Input.GetKeyDown(KeyCode.W)) dir = Vector2.up;
        if (Input.GetKeyDown(KeyCode.S)) dir = Vector2.down;
        if (Input.GetKeyDown(KeyCode.A)) dir = Vector2.left;
        if (Input.GetKeyDown(KeyCode.D)) dir = Vector2.right;

        if (dir != Vector2.zero)
        {
            HandleFlip(dir);   
            TryMove(dir);
        }
    }

    void HandleFlip(Vector2 dir)
    {
        if (dir.x > 0)          
        {
            isFacingRight = true;
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (dir.x < 0) 
        {
            isFacingRight = false;
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }

    void TryMove(Vector2 dir)
    {
        Vector2 start = GridUtils.Snap(transform.position);
        Vector2 target = start + dir;

        RaycastHit2D hit = Physics2D.Raycast(start, dir, 1f, blockingLayer);
        Debug.Log("Player hit: " + (hit.collider ? hit.collider.name : "NONE"));

        if (hit.collider == null)
        {
            animator?.SetBool("isRunning", true);
            StartCoroutine(MoveTo(target));
            return;
        }

        if (hit.collider.CompareTag("Box"))
        {
            BoxController box = hit.collider.GetComponent<BoxController>();

            if (box.TryPush(dir))
            {
                animator?.SetTrigger("Push");
                animator?.SetBool("isRunning", false);
                StartCoroutine(MoveTo(target));
            }
            else
            {
                animator?.SetBool("isRunning", false);
            }
        }
    }

    IEnumerator MoveTo(Vector2 target)
    {
        Debug.Log("test");
        isMoving = true;

        target = GridUtils.Snap(target);

        while (((Vector2)transform.position - target).sqrMagnitude > 0.0001f)
        {
            transform.position = Vector2.MoveTowards(
                transform.position,
                target,
                moveSpeed * Time.deltaTime);

            yield return null;
        }

        transform.position = target;

        animator?.SetBool("isRunning", false);
        isMoving = false;
    }
}
