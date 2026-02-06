using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//아이템 Instance 객체
public class ItemInstance 
{
    // GUID는 Unity 인스펙터에서 안 보이므로 string으로 보관
    [SerializeField] private string instanceId;

    public Guid InstanceId => Guid.Parse(instanceId);

    public ItemData Data { get; private set; }
    public int StackCount { get; private set; }

    public ItemInstance(ItemData data, int stackCount = 1)
    {
        instanceId = Guid.NewGuid().ToString(); 
        Data = data;
        StackCount = stackCount;
    }

    public bool CanStackWith(ItemInstance other)
    {
        //같은 에셋을 로드하기 때문에 같음
        return (Data == other.Data) && Data.stackable;
    }
}
