using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellData
{
    private Vector2Int pos;     // Grid 좌표
    private bool isWalkable;

    public Vector2Int Pos => pos;
    public bool IsWalkable => isWalkable;    //이동 가능 여부 (벽 여부)

    
    public CellData(int x, int y, bool isWalkable) {
        pos = new Vector2Int(x, y);
        this.isWalkable = isWalkable;
    }

    public CellData(Vector2Int pos, bool isWalkable)
    {
        this.pos = pos; this.isWalkable = isWalkable;
    }

    public HashSet<GameObject> occupants = new();
}
