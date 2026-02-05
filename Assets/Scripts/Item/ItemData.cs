using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    Equipment,
    Consumable,
    Material,
    Etc
}

public enum EquipmentType
{
    Weapon,
    Helmet,
    Armor,
    Ring
}

public enum Rarity
{
    Normal,
    Rare,
    Epic,
    Legendary
}

[CreateAssetMenu(menuName = "Item/ItemData")]
public class ItemData : ScriptableObject
{
    public int id;
    public string itemName;
    [TextArea] public string description;

    public Rarity rarity;

    public Sprite icon;

    public bool canSell;
    public int sellPrice;
}

public class EquipmentItemData : ItemData
{
    public EquipmentType equipmentType; 
    //public StatBlock baseStats;
}

public class ConsumableItemData : ItemData
{

}

public class MaterialItemData : ItemData
{

}
