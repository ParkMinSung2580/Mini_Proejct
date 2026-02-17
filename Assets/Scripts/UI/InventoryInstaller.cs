using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryInstaller : MonoBehaviour
{
    [SerializeField] private InventoryUI inventoryUI;

    private Inventory inventory;
    private InventoryPresenter presenter;

    private void Awake()
    {
        // 1. Model 持失
        inventory = new Inventory();

        // 2. Presenter 持失
        presenter = new InventoryPresenter(inventory, inventoryUI ,this);

        FindObjectOfType<InventoryTest>().inventory = inventory;
    }
}
