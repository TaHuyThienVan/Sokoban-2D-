using UnityEngine;

public static class GridUtils
{
    public static Vector2 Snap(Vector2 pos)
    {
        return new Vector2(
            Mathf.Round(pos.x),
            Mathf.Round(pos.y)
        );
    }
}
