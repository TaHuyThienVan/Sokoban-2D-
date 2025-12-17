using UnityEngine;

public class BoxSensor : MonoBehaviour
{
    public bool isBlocked { get; private set; }

    private void OnTriggerEnter2D(Collider2D other)
    {
        ObjectIdentity id = other.GetComponent<ObjectIdentity>();
        if (id != null && id.type == ObjectType.Wall)
        {
            isBlocked = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        ObjectIdentity id = other.GetComponent<ObjectIdentity>();
        if (id != null && id.type == ObjectType.Wall)
        {
            isBlocked = false;
        }
    }
}
