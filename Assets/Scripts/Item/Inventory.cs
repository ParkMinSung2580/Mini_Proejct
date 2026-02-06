using System;
using System.Collections.Generic;
using UnityEngine;

public class Inventory
{
    private List<InventoryBag> bags;

    private Dictionary<Guid, ItemInstance> items;

    public Inventory()
    {
        InventoryBag initBag = new(20);             //초기가방 
        bags = new List<InventoryBag>
        {
            initBag
        };

        items = new Dictionary<Guid, ItemInstance>();
    }

    public int GetBagsSize()
    {
        int size = 0;
        foreach(var bag in bags)
        {
            size += bag.Slots.Length;
        }
        return size;
    }

    public int GetEmptyBagsSize()
    {
        int size = 0;
        foreach (var bag in bags)
        {
            foreach(var Slot in bag.Slots)
            {
                if(Slot.IsEmpty)
                {
                    size++;
                }
            }
        }
        return size;
    }

    // 가방 순서 기준으로 가장 앞에 있는 빈 슬롯을 반환
    public InventorySlot FindFirstEmptySlot()
    {
        //처음 백부터 뒤져서 
        foreach (var bag in bags)
        {
            foreach (var slot in bag.Slots)
            {
                if (slot.IsEmpty)
                {
                    return slot;
                }
            }
        }
        return null;
    }

    /*public InventorySlot FindCombineSlot(ItemInstance item)
    {
        //같은 아이템이 존재하면 만약 Stackable인 친구이면 Combine할 Slot을 찾아줘야함.
        foreach (var bag in bags)
        {
            foreach (var slot in bag.Slots)
            {
                if (item == slot.Item)
                {
                    
                }
            }
        }
        return null;
    }*/

    //해당 아이템과 같은 Stack형 아이템이 bag안에 있는지 조사하는 함수
    private bool TryFindCombineSlot(ItemInstance incoming,out InventorySlot result)
    {
        result = null;

        if (incoming == null)
            return false;

        if (!incoming.Data.IsStackable)
            return false;

        foreach (var bag in bags)
        {
            foreach (var slot in bag.Slots)
            {
                if (slot.CanStack(incoming))
                {
                    result = slot;
                    return true;
                }
            }
        }
        return false;
    }

    private bool AddStackable(ItemInstance incoming)
    {
        while (incoming.Count > 0 && TryFindCombineSlot(incoming, out var combineSlot))
        {
            var target = combineSlot;

            int space = target.Item.MaxCount - target.Item.Count;
            int move = Mathf.Min(space, incoming.Count);

            target.Item.IncreaseStack(move);
            incoming.DecreaseStack(move);
        }

        // 아직 남아 있다면 새 슬롯 필요
        if (incoming.Count > 0)
        {
            var emptySlot = FindFirstEmptySlot();
            if (emptySlot == null)
                return false;

            emptySlot.Assign(incoming);
        }

        return true;
    }

    private bool AddAsNewSlot(ItemInstance item)
    {
        return false;
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
            return AddStackable(item);
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

        int targetSpace = target.Data.maxStack - target.Count;
        if (targetSpace <= 0) return;

        int moveCount = Mathf.Min(source.Count, targetSpace);

        source.DecreaseStack(moveCount);
        target.IncreaseStack(moveCount);

        if (source.Count <= 0)
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
        if (count >= item.Count) return;

        item.DecreaseStack(count);

        ItemInstance newItem = new ItemInstance(item.Data, count);

        items.Add(newItem.InstanceId, newItem);
    }
}
