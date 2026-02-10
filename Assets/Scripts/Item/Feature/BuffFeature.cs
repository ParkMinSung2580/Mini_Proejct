using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffFeature : IUseFeature
{
    public void Use()
    {
        Debug.Log("버프 아이템 사용");
    }
}
