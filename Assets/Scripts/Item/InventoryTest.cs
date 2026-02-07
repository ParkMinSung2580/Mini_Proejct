using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryTest : MonoBehaviour
{
    public Inventory inventory = new();

    public ItemData[] data;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(inventory.GetBagsSize());//inventory.GetBagsSize();
        Debug.Log(inventory.GetEmptyBagsSize());

        ItemInstance posion1 = new(data[0], 2);
        ItemInstance posion2 = new(data[0], 3);
        ItemInstance posion3 = new(data[1], 9);
        ItemInstance posion4 = new(data[1], 5);

        inventory.AddItem(posion1);
        inventory.AddItem(posion2);
        inventory.AddItem(posion3);
        inventory.AddItem(posion4);

        Debug.Log(inventory.GetBagsSize());//inventory.GetBagsSize();
        Debug.Log(inventory.GetEmptyBagsSize());

        Debug.Log($"[1번 슬롯] 아이템 이름 : {inventory[0].Item.Data.itemName.ToString()}, 아이템 개수 {inventory[0].Item.Count.ToString()}");
        Debug.Log($"[2번 슬롯] 아이템 이름 : {inventory[1].Item.Data.itemName.ToString()}, 아이템 개수 {inventory[1].Item.Count.ToString()}");
        Debug.Log($"[3번 슬롯] 아이템 이름 : {inventory[2].Item.Data.itemName.ToString()}, 아이템 개수 {inventory[2].Item.Count.ToString()}");
        Debug.Log($"[4번 슬롯] 아이템 이름 : {inventory[3].Item.Data.itemName.ToString()}, 아이템 개수 {inventory[3].Item.Count.ToString()}");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
