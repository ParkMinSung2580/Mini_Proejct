using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StackProperty : IItemProperty
{
    //public int Count { get; private set; } = 0;
    //public int MaxStack { get; private set; } = 1;

    private ItemInstance owner;

    public int Count { get; private set; }

    public int MaxStack => owner.Data.MaxStack;

    public bool CanStack => MaxStack > 1;

    public StackProperty(ItemInstance owner, int initialCount = 1)
    {
        this.owner = owner;
        SetInitialCount(initialCount);
    }

    /*
    public StackProperty(int initialCount = 1, int maxStack = 1)
    {
        MaxStack = maxStack;
        Count = Mathf.Clamp(initialCount, 0, maxStack);
    }*/

    public void SetInitialCount(int value)
    {
        Count = Mathf.Clamp(value, 1, MaxStack);
    }

    public bool DecreaseStack(int amount)
    {
        if (!CanStack) return false;
        if (amount <= 0) return false;
        if (Count < amount) return false;

        Count -= amount;
        return true;
    }

    public bool IncreaseStack(int amount)
    {
        if (!CanStack) return false;
        if (amount <= 0) return false;
        if (Count + amount > MaxStack) return false;

        Count += amount;
        return true;
    }
}
