using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Item/Feature/Buff")]
public class BuffFeatureSO : ItemFeatureSO
{
    public override IItemFeature CreateFeature()
       => new BuffFeature();
}
