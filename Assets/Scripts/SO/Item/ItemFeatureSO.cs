using UnityEngine;


public abstract class ItemFeatureSO : ScriptableObject
{
    public abstract IItemFeature CreateFeature();
}
