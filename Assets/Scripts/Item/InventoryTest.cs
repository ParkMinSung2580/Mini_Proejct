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

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
