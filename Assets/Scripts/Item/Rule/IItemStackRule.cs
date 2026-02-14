using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public interface IItemStackRule
{
    bool CanStack(ItemInstance a, ItemInstance b);
}

public class DefaultStackRule : IItemStackRule
{
    public bool CanStack(ItemInstance a, ItemInstance b)
    {
        if (a == null || b == null) return false;
        if (a.Data != b.Data) return false;
        if (!a.Data.IsStackable) return false;

        return a.CurrentCount < a.Data.MaxStack;
    }
}

public class EnhanceStackRule : IItemStackRule
{
    public bool CanStack(ItemInstance a, ItemInstance b)
    {
        if (!BaseRule(a, b)) return false;
        //return a.EnhanceLevel == b.EnhanceLevel;
        return false;
    }

    private bool BaseRule(ItemInstance a, ItemInstance b)
    {
        if (a.Data != b.Data) return false;
        if (!a.Data.IsStackable) return false;
        return true;
    }
}

public static class ItemStackRule
{
    public static bool CanStack(ItemInstance a, ItemInstance b)
    {
        if (a == null || b == null) { return false; }
        if (a.Data != b.Data) return false;

        var stackA = a.GetProperty<StackProperty>();
        var stackB = b.GetProperty<StackProperty>();

        Debug.Log(stackA == null || stackB == null);
        if (stackA == null || stackB == null) return false;

        return stackB.Count < stackB.MaxStack;
        //return stackA.Count < stackA.MaxStack;
    }
}