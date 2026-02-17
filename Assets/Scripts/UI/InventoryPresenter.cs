using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Unity.Collections.Unicode;

public class InventoryPresenter
{
    private Inventory model;
    private IInventoryView view;
    private MonoBehaviour runner;

    private float refreshInterval = 5f;
    private Coroutine refreshRoutine;

    public InventoryPresenter(Inventory model, IInventoryView view, MonoBehaviour runner)
    {
        this.model = model;
        this.view = view;
        this.runner = runner;

        view.CreateSlots(model.GetBagsSize());

        StartAutoRefresh();
    }

    public InventoryPresenter(Inventory model, IInventoryView view)
    {
        this.model = model;
        this.view = view;

        view.CreateSlots(model.GetBagsSize());

        Refresh();
    }

    private void StartAutoRefresh()
    {
        refreshRoutine = runner.StartCoroutine(AutoRefresh());
    }

    private IEnumerator AutoRefresh()
    {
        while (true)
        {
            Refresh();
            yield return new WaitForSeconds(refreshInterval);
        }
    }

    private void Refresh()
    {
        Debug.Log("인벤토리 Refresh");

        for (int i = 0; i < model.GetBagsSize(); i++)
        {
            var item = model.GetItem(i);

            if (item != null)
            {
                Debug.Log($"{i + 1}번째 슬롯에는 템이 존재합니다");
                view.SetSlot(i, item);
            }
            else
            {
                view.ClearSlot(i);
            }
        }
    }
}
