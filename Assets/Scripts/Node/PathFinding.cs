using System;
using System.Collections.Generic;
using UnityEngine;

public class PathFinding : MonoBehaviour
{
    private Dictionary<CellData, Node> nodeMap;
    private Node startNode,targetNode;

    private List<Node> finalNode; //마지막까지 찾은 노드
    private List<Node> openList, closedList;

    private Node currentNode;

    [SerializeField] private Transform target;
    [SerializeField] private bool diagonalMovement;

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

        //초기화
        nodeMap = new Dictionary<CellData, Node>();
        //오픈 리스트,클로즈 리스트 초기화
        openList = new List<Node>();
        closedList = new List<Node>();

        foreach (var kvp in GridManager.Instance.cells)
       {
            Vector2Int pos = kvp.Key; 
            CellData cell = kvp.Value;

            //Debug.Log($"좌표: {pos}, Walkable: {cell.IsWalkable}");

            nodeMap[cell] = new Node(cell);
            nodeMap[cell].G = int.MaxValue;
            nodeMap[cell].H = int.MaxValue;
            nodeMap[cell].parent = null;
        }

        finalNode = new List<Node>();
        startNode = nodeMap[startCell];
        targetNode = nodeMap[targetCell];

        startNode.G = 0;

        //추후 휴리스틱함수 추가
        startNode.H = Heuristic(startCell, targetCell);

        //오픈 리스트에 추가
        openList.Add(startNode);
        
        currentNode = startNode;
    }

    // → ↑ ← ↓순
    private bool FindingPath()
    {
        if (openList.Count == 0 )
        {
            Debug.LogWarning("오픈리스트에 갈 수 있는 노드가 존재하지 않습니다");
        }

        while (openList.Count > 0)
        {
            // 1. F가 가장 작은 노드 선택
            currentNode = GetLowestFNode(openList);

            // 2. open → closed 이동
            openList.Remove(currentNode);
            closedList.Add(currentNode);

            // 3. 목표 도착 체크
            if (currentNode == targetNode)
                return true;   

            // 4. 이웃 탐색
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
        /*//시작노드를 기반으로 근처의 모든 노드를 오픈리스트에 추가
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
        }*/
    }

    private void ShowPath()
    {
        if (currentNode.cell.Pos.x == targetNode.cell.Pos.x && currentNode.cell.Pos.y == targetNode.cell.Pos.y)
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
        Vector2Int target= new Vector2Int(x, y);
        CellData targetCell = GridManager.Instance.GetCell(target);

        //벽이면 해당 경로로 진입 불가 
        if (!targetCell.IsWalkable) return;

        //타겟 셀 좌표를 이웃노드로 만듬 
        Node neighbor = nodeMap[targetCell];

        //만약 닫힌 리스트에 해당 노드가 존재 하면 리턴
        if (closedList.Contains(neighbor)) return;

        int newG = currentNode.G + 10;

        if (newG < neighbor.G)
        {
            neighbor.G = newG;

            if (diagonalMovement)
            {
                neighbor.H = Heuristic2(targetCell, targetNode.cell);
            }
            else
            {
                neighbor.H = Heuristic(targetCell, targetNode.cell);
            }

            neighbor.parent = currentNode;

            if (!openList.Contains(neighbor))
                openList.Add(neighbor);
        }
    }

    //float 대신 Int값으로 
    //맨해튼 거리(Manhattan Distance) - |x1 - x2| + |y1 - y2| 두점 x1,y1과 x2,y2 사이의 맨해튼 거리
    private int Heuristic(CellData start,CellData target)
    {
        int value = (Mathf.Abs(start.Pos.x - target.Pos.x) + Mathf.Abs(start.Pos.y - target.Pos.y)) * 10;   //10을 곱하는건 정수로 스케일링
        //Debug.Log($"<color=yellow> Start CellPos x:{start.Pos.x}, y :{start.Pos.y}");
        //Debug.Log($"<color=green> Target CellPos x:{target.Pos.x}, y :{target.Pos.y}");
        Debug.Log($"맨해튼 휴리스틱 함수 값 : {value}");
        return value;
    }

    //유클리드 거리(Euclidean Distance) - 대각선 이동 허용 
    private int Heuristic2(CellData start,CellData target)
    {
        // 두 점 사이의 직선 거리 계산
        float dx = start.Pos.x - target.Pos.x; 
        float dy = start.Pos.y - target.Pos.y;

        // sqrt(dx^2 + dy^2)
        float distance = Mathf.Sqrt(dx * dx + dy * dy); //제곱근

        // 비용을 정수로 변환 
        int value = Mathf.RoundToInt(distance * 10);

        Debug.Log($"유클리드 휴리스틱 값 : {value}"); 
        return value;
    }

    private Node GetLowestFNode(List<Node> list)
    {
        Node best = list[0];

        foreach (Node node in list)
        {
            if (node.G + node.H < best.G + best.H)
                best = node;
        }

        return best;
    }

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
