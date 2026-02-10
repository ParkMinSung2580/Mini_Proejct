using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Unity.VisualScripting;
using UnityEngine;

public class InventorySlot
{
    public ItemInstance Item { get; private set; }

    public bool IsEmpty => Item == null;

    public bool CanStack(ItemInstance item)
    {
        /*return !IsEmpty && Item.CanStackWith(incoming);
        if (item == null || IsEmpty || Item.Data != item.Data || !Item.Data.IsStackable) return false;

        return Item.Count < Item.Data.maxStack;*/
        return ItemStackRule.CanStack(Item, item);
    }

    public void Assign(ItemInstance item)
    {
        if (item == null)
            throw new ArgumentNullException(nameof(item));

        Item = item;
    }

    public ItemInstance Clear()
    {
        var removed = Item;
        Item = null;
        return removed;
    }
}
