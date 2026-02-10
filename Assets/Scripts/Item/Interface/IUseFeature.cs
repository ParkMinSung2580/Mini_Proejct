using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IUseFeature : IItemFeature
{
    //Feature 필드에는 고정값만 사용
    public abstract void Use();
}
