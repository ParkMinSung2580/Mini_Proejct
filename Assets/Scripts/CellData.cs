using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellData
{
    public Vector3Int pos;     // Grid 좌표
    public bool isWalkable;    // 이동 가능 여부 (벽 여부)

    public HashSet<GameObject> occupants = new();
}
