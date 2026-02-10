using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

//아이템 Instance 객체
public class ItemInstance 
{
    private string instanceId;

    public Guid InstanceId => Guid.Parse(instanceId);

    public ItemData Data { get; private set; }

    private StackProperty stack;
    public int CurrentCount => stack.Count;

    private List<IItemProperty> Properties = new();
    private List<IItemFeature> Features = new();

    public T GetProperty<T>() where T : class, IItemProperty
        => Properties.Find(p => p is T) as T;

    public bool HasProperty<T>() where T : class, IItemProperty
    {
        return Properties.Any(p => p is T);
    }

    public IEnumerable<T> GetFeatures<T>() where T : class, IItemFeature
        => Features.OfType<T>();

    public bool HasFeature<T>() where T : class, IItemFeature
    {
        return Features.Any(f => f is T);
    }

    public ItemInstance(ItemData data, int initialCount = 1)
    {
        instanceId = Guid.NewGuid().ToString(); 
        Data = data;

        // SO에서 Property 생성
        Properties = data.Properties.Select(p => p.CreateProperty()).ToList();

        // SO에서 Feature 생성
        Features = data.Features.Select(f => f.CreateFeature()).ToList();

        stack = GetProperty<StackProperty>();
        if (stack == null)
            throw new Exception("ItemInstance에는 StackProperty가 반드시 존재해야 합니다.");
        else
            stack.SetInitialCount(initialCount);
    }
}
