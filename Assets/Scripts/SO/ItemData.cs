using System.Collections;
using System.Collections.Generic;
using UnityEditor;
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

public class ItemData : ScriptableObject
{
    [SerializeField] private string id;
    public string ID => id;

    [SerializeField] private string itemName;

    public string ItemName => itemName;

    [SerializeField][TextArea] private string description;

    public string Description => description;

    [SerializeField] private Rarity rarity;
    public Rarity Rarity => rarity;

    [SerializeField] private Sprite icon;
    public Sprite Icon => icon;

    [SerializeField] private List<ItemPropertySO> properties;
    public IReadOnlyList<ItemPropertySO> Properties => properties;

    [SerializeField] private List<ItemFeatureSO> features;
    public IReadOnlyList<ItemFeatureSO> Features => features;

    [SerializeField] private int maxStack = 1;
    public int MaxStack => maxStack;
    public bool IsStackable => maxStack > 1;

    /*
    [SerializeField] private bool canSell;
    public bool CanSell => canSell;

    [SerializeField] private int sellPrice;
    public int SellPrice => sellPrice;
    */

    /*
    public IEnumerable<IItemProperty> CreateProperties()
    {
        foreach (var p in Properties)
            yield return p.CreateProperty();

        if (MaxStack > 1)
            yield return new StackProperty(maxStack:MaxStack);
    }

    public IEnumerable<IItemFeature> CreateFeatures()
    {
        foreach (var f in Features)
            yield return f.CreateFeature();
    }
    */


    [SerializeField,Space(30)]
    [Header("type inspector ≥Î√‚")]
    private string type;

    private void OnValidate()
    {
        type = GetType().Name;
    }
}