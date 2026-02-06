using System;
using System.Collections.Generic;
using UnityEngine;


public class Inventory
{
    private Dictionary<Guid, ItemInstance> items;

    public Inventory()
    {
        items = new Dictionary<Guid, ItemInstance>();
    }

    private bool TryAddStackable(ItemInstance incoming)
    {
        foreach (var existing in items.Values)
        {
            if (existing.Data != incoming.Data) continue;
            if (existing.StackCount >= existing.Data.maxStack) continue;

            int space = existing.Data.maxStack - existing.StackCount;
            int move = Mathf.Min(space, incoming.StackCount);

            existing.IncreaseStack(move);
            incoming.DecreaseStack(move);

            if (incoming.StackCount <= 0)
                return true;
        }

        // 남은 개수가 있으면 새 슬롯으로
        return AddAsNewSlot(incoming);
    }

    private bool AddAsNewSlot(ItemInstance item)
    {
        if (items.ContainsKey(item.InstanceId))
            return false;

        items.Add(item.InstanceId, item);
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

    #region 외부 API
    /// <summary>
    /// 인벤토리에 아이템을 추가하는 API함수
    /// </summary>
    /// <param name="item"> Item이 IsStackable값에 따른 두가지 케이스 구분 </param>
    /// <returns></returns>
    public bool AddItem(ItemInstance item)
    {
        if (item.Data.IsStackable)
            return TryAddStackable(item);
        else
            return AddAsNewSlot(item);
    }

    public void CombineItem(Guid sourceId, Guid targetId)
    {
        if (!items.TryGetValue(sourceId, out var source)) return;
        if (!items.TryGetValue(targetId, out var target)) return;

        CombineItem(source, target);
    }

    public void DivideItem(Guid targetId,int count)
    {
        if (!items.TryGetValue(targetId, out var target)) return;

        DivideItem(target,count);
    }

    #endregion

    /// <summary>
    /// 같은 아이템이면 Stack 이동하도록 하는 내부 구현
    /// </summary>
    /// <param name="source">드래그한 아이템</param>
    /// <param name="target">놓여진 위치의 아이템</param>
    private void CombineItem(ItemInstance source, ItemInstance target)
    {
        if (source == target) return;
        if (source.Data != target.Data) return;
        if (!source.Data.IsStackable) return;

        int targetSpace = target.Data.maxStack - target.StackCount;
        if (targetSpace <= 0) return;

        int moveCount = Mathf.Min(source.StackCount, targetSpace);

        source.DecreaseStack(moveCount);
        target.IncreaseStack(moveCount);

        if (source.StackCount <= 0)
        {
            Remove(source.InstanceId);
        }
    }

    private bool Remove(Guid instanceId) 
    {
        return items.Remove(instanceId);
    }

    //인벤토리 나누기 기능
    public void DivideItem(ItemInstance item, int count)
    {
        if (item == null) return;
        if (!item.Data.IsStackable) return;
        if (count <= 0) return;
        if (count >= item.StackCount) return;

        item.DecreaseStack(count);

        ItemInstance newItem = new ItemInstance(item.Data, count);

        items.Add(newItem.InstanceId, newItem);
    }
}
