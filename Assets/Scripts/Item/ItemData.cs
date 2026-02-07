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
    public string id;
    public string itemName;
    [TextArea] public string description;

    public Rarity rarity;

    public Sprite icon;

    public int maxStack; //maxStack이 1일 시 stackable이 가능하지 않고 1보다 클 시 가능 
    public bool IsStackable => maxStack > 1;

    public bool canSell;
    public int sellPrice;
}

[CreateAssetMenu(menuName = "Item/ItemData/EquipmentItemData")]
public class EquipmentItemData : ItemData
{
    public EquipmentType equipmentType; 
    //public StatBlock baseStats;
}

[CreateAssetMenu(menuName = "Item/ItemData/ConsumableItemData")]
public class ConsumableItemData : ItemData
{

}

[CreateAssetMenu(menuName = "Item/ItemData/MaterialItemData")]
public class MaterialItemData : ItemData
{

}

[CreateAssetMenu(menuName = "Item/ItemData/Etc")]
public class EtcItemData : ItemData
{

}