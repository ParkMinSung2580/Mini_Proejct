using System;
using System.Collections.Generic;
using UnityEngine;

public class PathFinding : MonoBehaviour
{
    private Dictionary<Vector2Int, Node> nodeMap;
    private Node startNode,targetNode;

    private List<Node> finalNode; //마지막까지 찾은 노드
    //private List<Node> openList, closedList;

    private List<Node> closedList;
    private PriorityQueue<Node> openList;

    private Node currentNode;

    [SerializeField] private Transform target;
    [SerializeField] private bool diagonalMovement = false;

    //휴리스틱 모드 조정
    [SerializeField] private HeuristicMode heuristicMode = HeuristicMode.FH;

    private static readonly Vector2Int[] Dir4 =
    {
        Vector2Int.right,
        Vector2Int.up,
        Vector2Int.left,
        Vector2Int.down
    };

    private static readonly Vector2Int[] Dir8 =
    {
        Vector2Int.right,
        Vector2Int.up,
        Vector2Int.left,
        Vector2Int.down,
        new Vector2Int(1, 1),
        new Vector2Int(1, -1),
        new Vector2Int(-1, -1),
        new Vector2Int(-1, 1),
    };

    //Fisher-Yates shuffle
    private void Shuffle(Vector2Int[] array)
    {
        for (int i = array.Length - 1; i > 0; i--)
        {
            int j = UnityEngine.Random.Range(0, i + 1);
            (array[i], array[j]) = (array[j], array[i]);
        }
    }

    private void Start()
    {
        StartPath();

        bool pathFound = FindingPath();

        if (pathFound)
        {
            ShowPath();
        }
        else
        {
            Debug.LogWarning("목표 지점까지 갈 수 있는 경로가 없습니다.");
            finalNode.Clear();
        }

        Debug.Log($"확장된 CloseList Node 수 : {closedList.Count}");
    }
    private void StartPath()
    {
        //현재 경계의 월드 좌표를 cellPos로 변경(World → Grid 변환)
        Vector2Int startPos = GridManager.Instance.WorldToGrid(transform.position);

        //타겟 월드 좌표를 cellPos로 변경
        Vector2Int targetPos = GridManager.Instance.WorldToGrid(target.transform.position);

        //2단계: Grid → CellData
        CellData startCell = GridManager.Instance.GetCell(startPos);

        CellData targetCell = GridManager.Instance.GetCell(targetPos);

        if (startCell == null || targetCell == null) return;

        //초기화
        nodeMap = new Dictionary<Vector2Int, Node>();
        //오픈 리스트,클로즈 리스트 초기화    
        openList = new PriorityQueue<Node>(new NodeComparer(heuristicMode));
        closedList = new List<Node>();

        foreach (var kvp in GridManager.Instance.cells)
       {
            Vector2Int pos = kvp.Key; 
            CellData cell = kvp.Value;

            nodeMap[pos] = new Node(cell);
            nodeMap[pos].G = int.MaxValue;
            nodeMap[pos].H = int.MaxValue;
            nodeMap[pos].parent = null;
        }

        finalNode = new List<Node>();
        startNode = nodeMap[startCell.Pos];
        targetNode = nodeMap[targetCell.Pos];

        startNode.G = 0;

        startNode.H = diagonalMovement ? Octile(startCell, targetCell) : Manhattan(startCell, targetCell);

        //오픈 리스트에 추가
        openList.Enqueue(startNode); // node.F = node.G + node.H

        currentNode = startNode;
    }

    private bool FindingPath()
    {
        if (openList.Count == 0 )
        {
            Debug.LogWarning("오픈리스트에 갈 수 있는 노드가 존재하지 않습니다");
            return false;
        }

        while (openList.Count > 0)
        {
            // 1. F가 가장 작은 노드 선택
            //currentNode = GetLowestFNode(openList);
            currentNode = openList.Dequeue();

            //이미 닫힌 노드에 있으면 패스
            if (closedList.Contains(currentNode))
                continue;

            // 2. open → closed 이동
            //openList.Remove(currentNode);
            closedList.Add(currentNode);

            // 3. 목표 도착 체크
            if (currentNode == targetNode)
                return true;

            // 4. 이웃 탐색 
            // → ↑ ← ↓순
            //Vector2Int[] dirs = diagonalMovement ? Dir8 : Dir4;
            //Shuffle(dirs);

            /*foreach (var d in dirs)
            {
                OpenListAdd(
                    currentNode.cell.Pos.x + d.x,
                    currentNode.cell.Pos.y + d.y
                );
            }*/
            OpenListAdd(currentNode.cell.Pos.x + 1, currentNode.cell.Pos.y);
            OpenListAdd(currentNode.cell.Pos.x, currentNode.cell.Pos.y + 1);
            OpenListAdd(currentNode.cell.Pos.x - 1, currentNode.cell.Pos.y);
            OpenListAdd(currentNode.cell.Pos.x, currentNode.cell.Pos.y - 1);

            // ↗ ↘ ↙ ↖ 순
            if (diagonalMovement)
            {
                OpenListAdd(currentNode.cell.Pos.x + 1, currentNode.cell.Pos.y + 1);
                OpenListAdd(currentNode.cell.Pos.x + 1, currentNode.cell.Pos.y - 1);
                OpenListAdd(currentNode.cell.Pos.x - 1, currentNode.cell.Pos.y - 1);
                OpenListAdd(currentNode.cell.Pos.x - 1, currentNode.cell.Pos.y + 1);
            }
        }
        return false; //경로 없음
    }

    private void ShowPath()
    {
        if (currentNode == targetNode)
        {
            Node targetCurNode = currentNode;
            while (targetCurNode != startNode)
            {
                finalNode.Add(targetCurNode);
                targetCurNode = targetCurNode.parent;
            }
            finalNode.Add(startNode);
            finalNode.Reverse();

            for (int i = 0; i < finalNode.Count; i++)
            {
                Debug.Log($"{i} 번째는 {finalNode[i].cell.Pos.x}, {finalNode[i].cell.Pos.y}");
            }
        }
    }

    /// <summary>
    /// 노드를 추가 하는 함수
    /// </summary>
    /// <param name="x"> x좌표값 </param>
    /// <param name="y"> y좌표값 </param>
    private void OpenListAdd(int x, int y)
    {
        Vector2Int pos = new Vector2Int(x, y);
        CellData neighbor = GridManager.Instance.GetCell(pos);

        //해당 좌표를 가진 이웃셀이 존재하지 않을 때 return
        if (neighbor == null) return;

        //벽이면 해당 경로로 진입 불가 
        if (!neighbor.IsWalkable) return;

        //타겟 셀 좌표를 이웃노드로 만듬 
        Node neighborNode = nodeMap[neighbor.Pos];

        //만약 닫힌 리스트에 해당 노드가 존재 하면 리턴
        //if (closedList.Contains(neighborNode)) return;

        //대각선 이동인지 확인
        int dx = pos.x - currentNode.cell.Pos.x;
        int dy = pos.y - currentNode.cell.Pos.y;

        bool isDiagonal = Mathf.Abs(dx) == 1 && Mathf.Abs(dy) == 1;

        if (isDiagonal)
        {
            //두개만 확인 하면 된다 (좌우 둘중에 한칸,위아래 둘중에 한칸) 확인시 현재 노드의 
            CellData cell1 = GridManager.Instance.GetCell(new Vector2Int(currentNode.cell.Pos.x + dx, currentNode.cell.Pos.y));
            CellData cell2 = GridManager.Instance.GetCell(new Vector2Int(currentNode.cell.Pos.x, currentNode.cell.Pos.y + dy));

            if (cell1 == null || cell2 == null) return;
            if (!cell1.IsWalkable || !cell2.IsWalkable) return;
        }

        int moveCost = isDiagonal ? 14 : 10;
        int newG = currentNode.G + moveCost;

        if (newG < neighborNode.G)
        {
            neighborNode.G = newG;

            neighborNode.H = diagonalMovement ? Octile(neighbor, targetNode.cell) : Manhattan(neighbor, targetNode.cell);

            neighborNode.parent = currentNode;

            openList.Enqueue(neighborNode);           
        }

        //openList.DebugPrintHeap();
        //Debug.Log($"Heap valid: {openList.ValidateHeap()}");
    }

    //맨해튼 거리(Manhattan Distance) - |x1 - x2| + |y1 - y2| 두점 x1,y1과 x2,y2 사이의 맨해튼 거리
    private int Manhattan(CellData start,CellData target)
    {
        int dx = Mathf.Abs(start.Pos.x - target.Pos.x);
        int dy = Mathf.Abs(start.Pos.y - target.Pos.y);

        int value = 10 * (dx + dy);
        Debug.Log($"맨해튼 휴리스틱 함수 값 : {value}");

        return 10 * (dx + dy);
    }

    //유클리드 거리(Euclidean Distance) - 대각선 이동 허용 
    private int Euclidean(CellData start,CellData target)
    {
        // 두 점 사이의 직선 거리 계산
        float dx = Mathf.Abs(start.Pos.x - target.Pos.x); 
        float dy = Mathf.Abs(start.Pos.y - target.Pos.y);

        // sqrt(dx^2 + dy^2)
        float distance = Mathf.Sqrt(dx * dx + dy * dy); //제곱근

        // 비용을 정수로 변환 
        int value = Mathf.RoundToInt(distance * 10);

        Debug.Log($"유클리드 휴리스틱 값 : {value}"); 
        return value;
    }

    //옥타일 거리(Octile Distance) - Grid 기반 정교한 계산
    private int Octile(CellData start,CellData target)
    {
        int dx = Mathf.Abs(start.Pos.x - target.Pos.x);
        int dy = Mathf.Abs(start.Pos.y - target.Pos.y);

        int value = 
            14 * Mathf.Min(dx, dy) + 
            10 * (Mathf.Max(dx, dy) - Mathf.Min(dx, dy));

        Debug.Log($"옥타일 휴리스틱 값 : {value}");
        return value;
    }

    /*
    private Node GetLowestFNode(List<Node> list)
    {
        Node best = list[0];

        foreach (Node node in list)
        {
            if (node.G + node.H < best.G + best.H)
                best = node;
        }

        return best;
    }*/

    private void OnDrawGizmos()
    {
        if (GridManager.Instance == null) return;

        // Open List - 파랑
        if (openList != null)
        {
            Gizmos.color = new Color(0, 0, 1, 0.6f);
            foreach (var node in openList)
            {
                Gizmos.DrawCube(
                    GridManager.Instance.GridToWorld(node.cell.Pos),
                    Vector3.one * GridManager.Instance.cellSize * 0.3f
                );
            }
        }

        // Closed List - 빨강
        if (closedList != null)
        {
            Gizmos.color = new Color(1, 0, 0, 0.6f);
            foreach (var node in closedList)
            {
                Gizmos.DrawCube(
                    GridManager.Instance.GridToWorld(node.cell.Pos),
                    Vector3.one * GridManager.Instance.cellSize * 0.3f
                );
            }
        }

        // Final Path - 초록
        if (finalNode != null)
        {
            Gizmos.color = Color.green;
            foreach (var node in finalNode)
            {
                Gizmos.DrawCube(
                    GridManager.Instance.GridToWorld(node.cell.Pos),
                    Vector3.one * GridManager.Instance.cellSize * 0.45f
                );
            }
        }

        // Current Node - 노랑
        if (currentNode != null)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawCube(
                GridManager.Instance.GridToWorld(currentNode.cell.Pos),
                Vector3.one * GridManager.Instance.cellSize * 0.5f
            );
        }
    }
}
