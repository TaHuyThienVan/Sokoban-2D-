using UnityEngine;

public class BoxTrigger : MonoBehaviour
{
    public bool isOnGoal;

    void Start()
    {
        transform.position = GridUtils.Snap(transform.position);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        ObjectIdentity id = other.GetComponent<ObjectIdentity>();
        if (id != null && id.type == ObjectType.Goal)
        {
            Debug.Log("BOX VÀO GOAL");
            isOnGoal = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        ObjectIdentity id = other.GetComponent<ObjectIdentity>();
        if (id != null && id.type == ObjectType.Goal)
        {
            Debug.Log("BOX R?I GOAL");
            isOnGoal = false;
        }
    }
}
