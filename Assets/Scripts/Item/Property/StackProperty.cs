using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StackProperty : IItemProperty
{
    public int Count { get; set; }
    public int MaxStack { get; private set; }

    public bool CanStack => MaxStack > 1;

    public StackProperty(int initialCount, int maxStack)
    {
        MaxStack = maxStack;
        Count = Mathf.Clamp(initialCount, 0, maxStack);
    }

    public bool DecreaseStack(int amount)
    {
        if (amount <= 0) return false;
        if (Count < amount) return false;

        Count -= amount;
        return true;
    }

    public bool IncreaseStack(int amount)
    {
        if (amount <= 0) return false;
        if (Count + amount > MaxStack) return false;

        Count += amount;
        return true;
    }
}
