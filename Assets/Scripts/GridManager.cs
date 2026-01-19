using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GridManager : MonoBehaviour
{
    public static GridManager Instance;

    [Header("Grid Settings")]
    public float cellSize = 1f;
    public Vector3 origin = Vector3.zero;

    [Header("Tilemaps (Bake Only)")]
    public Tilemap ground;
    public Tilemap wall;

    public Dictionary<Vector2Int, CellData> cells = new();

    void Awake()
    {
        Instance = this;
        BuildCellsFromTilemap();
    }

    private void BuildCellsFromTilemap()
    {
        cells.Clear();

        BoundsInt bounds = ground.cellBounds;

        foreach (var pos in bounds.allPositionsWithin)
        {
            if (!ground.HasTile(pos))
                continue;

            Vector2Int cellPos = new Vector2Int(pos.x, pos.y);

            cells[cellPos] = new CellData(cellPos, !wall.HasTile(pos));
        }
    }

    /* ================= Grid API ================= */

    public bool HasCell(Vector2Int pos)
    {
        return cells.ContainsKey(pos);
    }

    public CellData GetCell(Vector2Int pos)
    {
        cells.TryGetValue(pos, out var cell);
        return cell;
    }

    public bool IsWalkable(Vector2Int pos)
    {
        return cells.TryGetValue(pos, out var cell) && cell.IsWalkable;
    }

    /* ================= Coordinate Convert ================= */

    public Vector2Int WorldToGrid(Vector3 worldPos)
    {
        int x = Mathf.FloorToInt((worldPos.x - origin.x) / cellSize);
        int y = Mathf.FloorToInt((worldPos.y - origin.y) / cellSize);
        return new Vector2Int(x, y);
    }

    public Vector3 GridToWorld(Vector2Int gridPos)
    {
        return new Vector3(
            origin.x + gridPos.x * cellSize + cellSize * 0.5f,
            origin.y + gridPos.y * cellSize + cellSize * 0.5f,
            0
        );
    }

    /* ================= Debug ================= */

    /*private void OnDrawGizmos()
    {
        if (cells == null) return;

        foreach (var cell in cells.Values)
        {
            Gizmos.color = cell.IsWalkable
                ? new Color(0, 1, 0, 0.35f)
                : new Color(1, 0, 0, 0.5f);

            Gizmos.DrawCube(
                GridToWorld(cell.Pos),
                Vector3.one * cellSize * 0.5f
            );

            Gizmos.color = Color.black;
            Gizmos.DrawWireCube(
                GridToWorld(cell.Pos),
                Vector3.one * cellSize * 0.5f
            );
        }
    }*/
}
