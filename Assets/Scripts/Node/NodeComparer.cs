using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeComparer : IComparer<Node>
{
    private HeuristicMode mode;
    private double epsilon; // 작은 값 (예: 0.001 ~ 0.1)
    public NodeComparer(HeuristicMode mode, double epsilon = 0.001)
    {
        this.mode = mode;
        this.epsilon = epsilon;
    }

    public int Compare(Node a, Node b)
    {
        switch (mode)
        {
            case HeuristicMode.FH:
                int cmpFH = a.F.CompareTo(b.F); // a.F > b.F 이면 1, 같으면 0, 작으면 -1
                if (cmpFH != 0) return cmpFH;   // F 값이 다르면 H는 고려하지 않음
                //return b.H.CompareTo(a.H);      // F 값이 같으면 H값에 따라서 우선순위가 매겨짐
                return a.H.CompareTo(b.H);

            case HeuristicMode.FG:
                int cmpFG = a.F.CompareTo(b.F);
                if (cmpFG != 0) return cmpFG;
                return a.G.CompareTo(b.G);

            case HeuristicMode.H:
                return a.H.CompareTo(b.H);

            case HeuristicMode.F:
                return a.F.CompareTo(b.F);

            default:
                return a.F.CompareTo(b.F);
        }
    }
}

