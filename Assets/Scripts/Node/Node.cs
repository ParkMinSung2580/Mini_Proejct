using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    public CellData cell; // 원본 Cell 참조
    public Node parent; // 경로 추적용 부모 노드
    public int G; // 시작점으로부터 비용
    public int H; // 목표까지의 휴리스틱 비용
    public int F { get { return G + H; } } 
    public Node(CellData _cell) 
    {
        { cell = _cell; } 
    }
}
