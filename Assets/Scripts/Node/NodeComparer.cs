using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeComparer : IComparer<Node>
{
    private HeuristicMode mode;

    public NodeComparer(HeuristicMode mode)
    {
        this.mode = mode;
    }

    public int Compare(Node a, Node b)
    {
        //F 비용 우선 비교
        int result = a.F.CompareTo(b.F);
        if (result != 0) return result;

        switch (mode)
        {
            case HeuristicMode.F:
                return 0;

            case HeuristicMode.FH:
                return a.H.CompareTo(b.H); // H 작은 쪽 우선

            case HeuristicMode.FG:
                return a.G.CompareTo(b.G); // G 작은 쪽 우선

            case HeuristicMode.FHG:
                result = a.H.CompareTo(b.H); // H 작은 쪽 우선
                if (result != 0) return result;

                return b.G.CompareTo(a.G); // G 큰 쪽 우선 

            default:
                return 0;
        }
    }
}

