using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PriorityQueue<T>
{
    private List<(T item, int priority)> heap = new List<(T, int)>();
    private int Compare(int i, int j) => heap[i].priority.CompareTo(heap[j].priority);

    private void Enqueue(T item,int priority)
    {
        //리스트 끝부분에 추가
        heap.Add((item,priority));

        //끝 부분의 인덱스를 들고오고 
        int i = heap.Count - 1;

        //비교를 한다
        while (i > 0)
        {
            int parent = (i - 1) / 2;

            //현재 i가 parent보다 우선순위가 크면 멈추고 
            if (heap[i].priority >= heap[parent].priority) break;

            //그렇지 않으면 변경해줘야한다 swap
            var temp = heap[i]; 
            heap[i] = heap[parent]; 
            heap[parent] = temp;

            i = parent;
        }
    }

    private T Dequeue(T item)
    {
        int last = heap.Count - 1;
        
        // 루트노드를 빼야하기 때문에 swap 해줘야한다 맨마지막 노드랑 Tree노드랑
        T item = heap[last].item; 
        heap.RemoveAt(last);

        return null;
    }
}
