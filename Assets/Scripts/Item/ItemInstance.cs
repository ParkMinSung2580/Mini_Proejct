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

    private List<IItemProperty> Properties = new();
    private List<IItemFeature> Features = new();

    public T GetProperty<T>() where T : class, IItemProperty
        //    => Properties.Find(p => p is T) as T;
    {
        Debug.Log(Properties.Find(p => p is T) as T);
        return Properties.Find(p => p is T) as T;      
    }

    public IEnumerable<T> GetFeatures<T>() where T : class, IItemFeature
        //    => Features.OfType<T>().ToList();
    {
        Debug.Log($"GetFeatures<{typeof(T).Name}> called");
        Debug.Log($"Features count: {Features?.Count ?? -1}");

        var result = Features?.OfType<T>().ToList() ?? new List<T>();

        Debug.Log($"Found {result.Count} features");
        return result;
    }

    public ItemData Data { get; private set; }


    //public int Count { get; set; }

    //public int MaxCount { get => Data.maxStack; }

    public bool CanStackWith(ItemInstance other)
    {
        if (other == null) return false;
        if (Data != other.Data) return false;
        if (!Data.IsStackable) return false;

        return Count < Data.maxStack;
    }

    public ItemInstance(ItemData data, int stackCount = 1)
    {
        instanceId = Guid.NewGuid().ToString(); 
        Data = data;
        Count = stackCount;

        // SO에서 Property 생성
        Properties = data.Properties.Select(p => p.CreateProperty()).ToList();

        // SO에서 Feature 생성
        Features = data.Features.Select(f => f.CreateFeature()).ToList();
  
    }

    public void DecreaseStack(int amount)
    {
        Count -= amount;
    }

    public void IncreaseStack(int amount)
    {
        Count += amount;
    }
}
