using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryBag
{
    public InventorySlot[] Slots { get; }

    public InventoryBag(int size)
    {
        Slots = new InventorySlot[size];
        for (int i = 0; i < size; i++)
            Slots[i] = new InventorySlot();
    }
}
