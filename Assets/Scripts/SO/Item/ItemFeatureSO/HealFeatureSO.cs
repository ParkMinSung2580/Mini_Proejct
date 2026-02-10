using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Item/Feature/Heal")]
public class HealFeatureSO : ItemFeatureSO
{
    public override IItemFeature CreateFeature()
        => new HealFeature();
}
