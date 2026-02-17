using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class InventoryUI : MonoBehaviour, IInventoryView
{
    [SerializeField] private InventorySlotUI slotPrefab;
    [SerializeField] private Transform slotParent;

    private List<InventorySlotUI> slots = new();

    public event Action<int> OnSlotClicked;

    public void CreateSlots(int count)
    {
        for (int i = 0; i < count; i++)
        {
            var slot = Instantiate(slotPrefab, slotParent);
            slot.name = "Slot" + (i+1).ToString();
            slot.Init(i, HandleSlotClicked);
            slots.Add(slot);
        }
    }

    private void HandleSlotClicked(int index)
    {
        OnSlotClicked?.Invoke(index);
    }

    public void ClearSlot(int index)
    {
        slots[index].Clear();
    }

    public void SetSlot(int index, ItemInstance item)
    {
        slots[index].Set(item, item.CurrentCount);
    }
}
    
