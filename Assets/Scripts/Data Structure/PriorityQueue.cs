using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PriorityQueue<T> : IEnumerable<T>
{
    private List<T> heap = new List<T>();
    private IComparer<T> comparer;
    // 생성자: 비교 규칙을 외부에서 주입
    public PriorityQueue(IComparer<T> comparer) 
    { 
        this.comparer = comparer;  
    }

    public int Count => heap.Count;
    //private int Compare(int i, int j) => heap[i].priority.CompareTo(heap[j].priority);

    public void Enqueue(T item)
    {
        //리스트 끝부분에 추가
        heap.Add(item);

        //끝 부분의 인덱스를 들고오고 
        int i = heap.Count - 1;

        //비교를 한다
        while (i > 0)
        {
            int parent = (i - 1) / 2;

            //현재 i가 parent보다 우선순위가 크면 멈추고 
            //if (heap[i].priority >= heap[parent].priority) break;
            if (comparer.Compare(heap[i], heap[parent]) >= 0) break;

            //그렇지 않으면 변경해줘야한다 swap
            (heap[i], heap[parent]) = (heap[parent], heap[i]);
            i = parent;
        }

        if (heap[i] is Node node)
        {
            Debug.Log($"<color=yellow>들어간 노드의 H 휴리스틱 값 : {node.H}</color>");
        }
    }

    public T Dequeue()
    {
        //깜빡 추가     
        if (heap.Count == 0)
        {
            Debug.Log("heap이 비어 있습니다");
        }

        int last = heap.Count - 1;

        // 루트노드를 빼야하기 때문에 swap 해줘야한다 맨마지막 노드랑 Tree노드랑
        (heap[0], heap[last]) = (heap[last], heap[0]); //swap

        var root = heap[last];
        heap.RemoveAt(last);

        //나가는 조건은 자식이 없을 경우 나 보다 작은 자식을 없을 경우
        int i = 0;
        while (true)
        {
            int left = i * 2 + 1;
            int right = i * 2 + 2;
            if (left >= heap.Count) break;

            //작은걸 왼쪽아래로
            int smallest = left;

            //오른쪽 왼쪽 자식이 있는지 확인해 보고 둘중에 어떤 것이 더 큰지 확인한다.
            //만약 왼쪽보다 오른쪽이 더 작으면 오른쪽자식으로 이동해야한다.
            if (right < heap.Count && comparer.Compare(heap[right], heap[left]) < 0) smallest = right;

            //현재 내가 자식보다 우선순위가 높으면 반복을 멈춘다.
            if (comparer.Compare(heap[i], heap[smallest]) <= 0) break;

            //위치 변경 swap
            (heap[i], heap[smallest]) = (heap[smallest], heap[i]);

            //i값을 변경하여 반복한다.
            i = smallest;
        }
        return root;
    }

    public void DebugPrintHeap()
    {
        int index = 0;
        foreach (var item in heap)
        {
            if (item is Node node)
            {
                Debug.Log(
                    $"<color=yellow>" +
                    $"F:{node.F} G:{node.G} H:{node.H}" +
                    $"</color>"
                );
            }
            index++;
        }
    }

    public bool ValidateHeap()
    {
        if (heap.Count == 0) return true;
            
        for (int i = 0; i < heap.Count; i++)
        {
            int left = i * 2 + 1;
            int right = i * 2 + 2;

            if (left < heap.Count && comparer.Compare(heap[i], heap[left]) > 0)
                return false;

            if (right < heap.Count && comparer.Compare(heap[i], heap[right]) > 0)
                return false;
        }
        return true;
    }

    public IEnumerator<T> GetEnumerator() => heap.GetEnumerator(); 
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    /*
        https://learn.microsoft.com/ko-kr/dotnet/fundamentals/code-analysis/style-rules/ide0180

        //Swap 관련 코드
        var temp = heap[i]; 
        heap[i] = heap[parent]; 
        heap[parent] = temp;

        Violates IDE0180.
        int temp = numbers[0];
        numbers[0] = numbers[1];
        numbers[1] = temp;

        // Fixed code.
        (numbers[1], numbers[0]) = (numbers[0], numbers[1]);
    */
}
