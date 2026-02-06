using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryTest : MonoBehaviour
{
    public Inventory inventory = new();
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(inventory.GetBagsSize());//inventory.GetBagsSize();
        Debug.Log(inventory.GetEmptyBagsSize());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
