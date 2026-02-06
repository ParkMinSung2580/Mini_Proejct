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

    public int maxStack; //maxStack이 1일 시 stackable이 가능하지 않고 1보다 클 시 가능 
    public bool IsStackable => maxStack > 1;

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
