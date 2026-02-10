using UnityEngine;

public class EnhanceProperty : IItemProperty
{
    public int Level { get; private set; }
    public int MaxLevel { get; }

    public EnhanceProperty(int maxLevel)
    {
        MaxLevel = maxLevel;
        Level = 0;
    }

    public bool TryEnhance()
    {
        if (Level >= MaxLevel)
            return false;

        Level++;
        return true;
    }
}
