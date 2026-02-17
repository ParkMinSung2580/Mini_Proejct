using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryTest : MonoBehaviour
{
    public Inventory inventory;

    public ItemData[] data;

    void Awake()
    {
        //inventory = new Inventory(); // Awake에서 초기화
    }
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(inventory.GetBagsSize());//inventory.GetBagsSize();
        Debug.Log(inventory.GetEmptyBagsSize());

        ItemInstance posion1 = new(data[0], 10);
        ItemInstance posion2 = new(data[0], 3);
        ItemInstance posion3 = new(data[1], 9);
        ItemInstance posion4 = new(data[1], 5);

        /*for (int i = 0; i < 12; i++)
        {
            //new ItemInstance만 사용해야한다.
            inventory.AddItem(new(data[0],10));
        }*/

        inventory.AddItem(posion1);
        inventory.AddItem(posion2);
        inventory.AddItem(posion3);
        inventory.AddItem(posion4);

        //Debug.Log(inventory.GetBagsSize());//inventory.GetBagsSize();
        Debug.Log(inventory.GetEmptyBagsSize());
        
        Debug.Log($"[1번 슬롯] 아이템 이름 : {inventory[0].Item.Data.ItemName.ToString()}, 아이템 개수 {inventory[0].Item.CurrentCount}");
        Debug.Log($"[2번 슬롯] 아이템 이름 : {inventory[1].Item.Data.ItemName.ToString()}, 아이템 개수 {inventory[1].Item.CurrentCount}");
        Debug.Log($"[3번 슬롯] 아이템 이름 : {inventory[2].Item.Data.ItemName.ToString()}, 아이템 개수 {inventory[2].Item.CurrentCount}");
        Debug.Log($"[4번 슬롯] 아이템 이름 : {inventory[3].Item.Data.ItemName.ToString()}, 아이템 개수 {inventory[3].Item.CurrentCount}");
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q))
        {
            inventory.UseItem(inventory[0].Item);
            Debug.Log($"[1번 슬롯] 아이템 이름 : {inventory[0].Item.Data.ItemName}, 아이템 개수 {inventory[0].Item.CurrentCount}");
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            inventory.UseItem(inventory[1].Item);
            Debug.Log($"[2번 슬롯] 아이템 이름 : {inventory[1].Item.Data.ItemName}, 아이템 개수 {inventory[1].Item.CurrentCount}");
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            inventory.UseItem(inventory[2].Item);
            Debug.Log($"[3번 슬롯] 아이템 이름 : {inventory[2].Item.Data.ItemName}, 아이템 개수 {inventory[2].Item.CurrentCount}");
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            inventory.UseItem(inventory[3].Item);
            Debug.Log($"[4번 슬롯] 아이템 이름 : {inventory[3].Item.Data.ItemName}, 아이템 개수 {inventory[3].Item.CurrentCount}");
        }
    }
}

