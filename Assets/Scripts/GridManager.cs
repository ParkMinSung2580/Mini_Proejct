using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GridManager : MonoBehaviour
{
    public static GridManager Instance;

    public Grid grid;
    public Tilemap ground;
    public Tilemap wall;

    public Dictionary<Vector3Int, CellData> cells = new();

    void Awake()
    {
        Instance = this;
        BuildCells();
    }

    void BuildCells()
    {
        BoundsInt bounds = ground.cellBounds;

        foreach (var pos in bounds.allPositionsWithin)
        {
            if (!ground.HasTile(pos)) continue;

            cells[pos] = new CellData
            {
                pos = pos,
                isWalkable = !wall.HasTile(pos)
            };
        }
    }
}
