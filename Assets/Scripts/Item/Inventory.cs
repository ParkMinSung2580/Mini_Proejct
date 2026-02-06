using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    private Dictionary<Guid,ItemInstance> items = new Dictionary<Guid, ItemInstance>();

    public Dictionary<Guid, ItemInstance> Items => items;

    public bool AddItem(ItemInstance item)
    {
        if (items.ContainsKey(item.InstanceId))
        {
            Debug.LogWarning($"이미 존재하는 ItemInstanceId : {item.InstanceId}");
            return false;
        }

        items.Add(item.InstanceId, item);

        Debug.Log($"인벤토리 추가: {item.Data.itemName} ({item.InstanceId})");
        return true;
    }
    public bool RemoveItem(Guid instanceId)
    {
        if (!items.Remove(instanceId))
        {
            Debug.LogWarning($"삭제 실패 - 존재하지 않는 ItemInstanceId : {instanceId}");
            return false;
        }

        Debug.Log($"인벤토리 삭제: {instanceId}");
        return true;
    }

    //이미 존재할때에는 해당 수의 MaxStack을 보고 새로운 객체 데이터를 추가해야하나?
}
