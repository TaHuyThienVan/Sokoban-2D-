using UnityEngine;
//tam thoi chua dung. dang bi loi

public class GridManager : MonoBehaviour
{
    public static GridManager Instance;

    public int gridX = 12;
    public int gridY = 8;

    private float cellW;
    private float cellH;

    private float worldW;
    private float worldH;

    void Awake()
    {
        Instance = this;
        CalculateGrid();
    }

    void CalculateGrid()
    {
        worldH = Camera.main.orthographicSize * 2f;
        worldW = worldH * Camera.main.aspect;

        cellW = worldW / gridX;
        cellH = worldH / gridY;
    }

    public Vector2 GetCellCenter(int x, int y)
    {
        float left = -worldW / 2f;
        float bottom = -worldH / 2f;

        return new Vector2(
            left + x * cellW + cellW / 2f,
            bottom + y * cellH + cellH / 2f
        );
    }
    public Vector2 SnapToGrid(Vector2 pos)
    {
        float worldHeight = worldH;
        float worldWidth = worldW;

        float left = -worldWidth / 2f;
        float bottom = -worldHeight / 2f;

        int x = Mathf.RoundToInt((pos.x - left) / cellW);
        int y = Mathf.RoundToInt((pos.y - bottom) / cellH);

        return GetCellCenter(x, y);
    }
}
