using System.Threading;
using UnityEngine;

[CreateAssetMenu(menuName = "Item/Property/Stack")]
public class StackPropertySO : ItemPropertySO
{
    public int maxStack;

    public override IItemProperty CreateProperty()
        => new StackProperty(maxStack:this.maxStack);
}
