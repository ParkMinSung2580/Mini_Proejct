using UnityEngine;

[CreateAssetMenu(menuName = "Item/Property/Stack")]
public class StackPropertySO : ItemPropertySO
{
    public int maxStack;

    public override IItemProperty CreateProperty()
        => new StackProperty(0,maxStack);
}
