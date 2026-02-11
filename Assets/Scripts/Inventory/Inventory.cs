using System;
using System.Collections.Generic;
using UnityEngine;

public class Inventory
{
    private List<InventoryBag> bags;

    //전체 인벤토리를 하나의 배열처럼 접근 반환값 Slot
    public InventorySlot this[int index]
    {
        get
        {
            int currentIndex = 0;                       // 현재까지 확인한 슬롯 개수
            foreach (var bag in bags)
            {
                // 각 가방의 슬롯을 뒤진다. 
                if (index < currentIndex + bag.Size)
                {
                    // 가방 내부의 로컬 인덱스로 변환
                    // 전체 인덱스 - 이전까지의 슬롯 개수 = 가방 내 위치
                    return bag[index - currentIndex];
                }
                // 이 가방에 없으면 다음 가방으로
                currentIndex += bag.Size;
            }
            // 모든 가방을 다 찾아봤는데 없으면 에러
            throw new IndexOutOfRangeException($"슬롯 인덱스 {index}가 범위를 벗어났습니다.");
        }
    }

    public Inventory()
    {
        #region 테스트 Init()
        InventoryBag initBag = new(20);             //초기가방 
        bags = new List<InventoryBag>
        {
            initBag
        };
        #endregion
    }

    public int GetBagsSize()
    {
        int size = 0;
        foreach (var bag in bags)
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
            foreach (var Slot in bag.Slots)
            {
                if (Slot.IsEmpty)
                {
                    size++;
                }
            }
        }
        return size;
    }

    //아이템 접근 api
    public ItemInstance GetItem(int index)
    {
        return this[index].Item;
    }

    public void UseItem(ItemInstance item)
    {
        foreach (var feature in item.GetFeatures<IUseFeature>())
        {
            feature.Use();
        }

        var stack = item.GetProperty<StackProperty>();
        stack?.DecreaseStack(1);
    }

    private InventorySlot FindFirstEmptySlot()
    {
        //처음 백부터 뒤져서 
        foreach (var bag in bags)
        {
            foreach (var slot in bag.Slots)
            {
                if (slot.IsEmpty) return slot;
            }
        }
        return null;
    }

    //해당 아이템과 같은 Stack형 아이템이 bag안에 있는지 조사하는 함수 out파라메터로 Slot 반환
    //Count값 까지 비교
    public bool TryFindCombineSlot(ItemInstance incoming, out InventorySlot result)
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

    private bool AddStackableItem(ItemInstance incoming)
    {
        var from = incoming.GetProperty<StackProperty>();

        while (from.Count > 0 && TryFindCombineSlot(incoming, out var combineSlot))
        {
            var to = combineSlot.Item.GetProperty<StackProperty>();

            int space = to.MaxStack - to.Count;
            int move = Mathf.Min(space, from.Count);

            to.IncreaseStack(move);
            from.DecreaseStack(move);
        }

        // 아직 남아 있다면 새 슬롯 필요
        if (from.Count > 0)
        {
            var emptySlot = FindFirstEmptySlot();
            if (emptySlot == null)
                return false;

            int remain = from.Count;

            // 기존 incoming은 0으로 만들고
            from.DecreaseStack(remain);

            ItemInstance newItem = new ItemInstance(incoming.Data, remain);

            emptySlot.Assign(newItem);
        }

        return true;
    }


    private bool AddAsNewSlot(ItemInstance item)
    {
        InventorySlot newSlot = FindFirstEmptySlot();

        if (newSlot == null) return false;

        newSlot.Assign(item);

        return true;
    }

    public bool RemoveItem(Guid instanceId)
    {
        foreach (var bag in bags)
        {
            foreach (var slot in bag.Slots)
            {
                if (!slot.IsEmpty && slot.Item.InstanceId == instanceId)
                {
                    slot.Clear();
                    return true;
                }
            }
        }
        return false;
    }

    public bool RemoveItem(InventorySlot slot, Index count)
    {
        return false;
    }

    #region 외부 API
    /// <summary>
    /// 인벤토리에 아이템을 추가하는 API함수
    /// </summary>
    /// <param name="item"> Item이 IsStackable값에 따른 두가지 케이스 구분 </param>
    /// <returns></returns>
    public bool AddItem(ItemInstance item)
    {
        if (GetEmptyBagsSize() == 0)
        {
            Debug.Log("더 이상 인벤토리에 넣을 수 없습니다.");
            return false;
        }

        Debug.Log("ADD호출");
        if (item == null)
            return false;

        var stack = item.GetProperty<StackProperty>();
        if (stack == null)
            throw new Exception("ItemInstance에는 StackProperty가 반드시 있어야 합니다.");

        if (stack.CanStack)
            return AddStackableItem(item);
        else
            return AddAsNewSlot(item);
    }

    public void CombineItem(Guid sourceId, Guid targetId)
    {
        //if (!items.TryGetValue(sourceId, out var source)) return;
        //if (!items.TryGetValue(targetId, out var target)) return;

        //CombineItem(source, target);
    }

    /*public void DivideItem(Guid targetId,int count)
    {
        if (!items.TryGetValue(targetId, out var target)) return;

        DivideItem(target,count);
    }*/

    #endregion

    /// <summary>
    /// 같은 아이템이면 Stack 이동하도록 하는 내부 구현
    /// </summary>
    /// <param name="source">드래그한 아이템</param>
    /// <param name="target">놓여진 위치의 아이템</param>
    private void CombineItem(ItemInstance source, ItemInstance target)
    {
        /*if (source == target) return;
        if (source.Data != target.Data) return;
        if (!source.Data.IsStackable) return;

        int targetSpace = target.Data.maxStack - target.Count;
        if (targetSpace <= 0) return;

        int moveCount = Mathf.Min(source.Count, targetSpace);

        source.DecreaseStack(moveCount);
        target.IncreaseStack(moveCount);

        if (source.Count <= 0)
        {
            //Remove(source.InstanceId);
        }*/
    }

    /*private bool Remove(Guid instanceId) 
    {
        return items.Remove(instanceId);
    }*/

    //인벤토리 나누기 기능
    /*public void DivideItem(ItemInstance item, int count)
    {
        if (item == null) return;
        if (!item.Data.IsStackable) return;
        if (count <= 0) return;
        if (count >= item.Count) return;

        item.DecreaseStack(count);

        ItemInstance newItem = new ItemInstance(item.Data, count);

        items.Add(newItem.InstanceId, newItem);
    }*/
}
