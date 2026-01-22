using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Test
{
    public class Test : MonoBehaviour
    {
        void Start()
        {
            TestNameSort();
            TestAgeSort();
            TestBothSort();
        }

        void TestAgeSort()
        {
            Debug.Log("=== Age Sort Test (나이 큰 순) ===");

            var pq = new PriorityQueue<Person>(
                new PersonComparer(PersonSortMode.Age)
            );

            pq.Enqueue(new Person("Alice", 25));
            pq.Enqueue(new Person("Bob", 40));
            pq.Enqueue(new Person("Charlie", 30));

            while (pq.Count > 0)
            {
                Person p = pq.Dequeue();
                Debug.Log($"{p.Name} / {p.Age}");

                Debug.Log($"Heap valid: {pq.ValidateHeap()}");
            }
        }

        void TestNameSort()
        {
            Debug.Log("=== Name Sort Test (A → Z) ===");

            var pq = new PriorityQueue<Person>(
                new PersonComparer(PersonSortMode.Name)
            );

            pq.Enqueue(new Person("Charlie", 30));
            pq.Enqueue(new Person("Alice", 25));
            pq.Enqueue(new Person("Bob", 40));

            while (pq.Count > 0)
            {
                Person p = pq.Dequeue();
                Debug.Log($"{p.Name} / {p.Age}");

                Debug.Log($"Heap valid: {pq.ValidateHeap()}");
            }
        }

        void TestBothSort()
        {
            Debug.Log("=== Both Sort Test (Age ↓, Name ↑) ===");

            var pq = new PriorityQueue<Person>(
                new PersonComparer(PersonSortMode.Both)
            );

            pq.Enqueue(new Person("Alice", 30));
            pq.Enqueue(new Person("Bob", 30));
            pq.Enqueue(new Person("Charlie", 40));
            pq.Enqueue(new Person("Dave", 40));

            while (pq.Count > 0)
            {
                Person p = pq.Dequeue();
                Debug.Log($"{p.Name} / {p.Age}");

                Debug.Log($"Heap valid: {pq.ValidateHeap()}");
            }
        }
    }
}
