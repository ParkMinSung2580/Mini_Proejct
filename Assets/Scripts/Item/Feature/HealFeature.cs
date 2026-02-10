using UnityEngine;

public class HealFeature : IUseFeature
{
    public void Use()
    {
        Debug.Log("힐아이템 사용");
    }
}