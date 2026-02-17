using System.Threading;
using UnityEngine;

[CreateAssetMenu(menuName = "Item/Property/Stack")]
public class StackPropertySO : ItemPropertySO
{
    public override IItemProperty CreateProperty(ItemInstance owner)
        => new StackProperty(owner);
}
