using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class InventorySlotUI : MonoBehaviour
{
    [SerializeField] private Image icon;
    [SerializeField] private TextMeshProUGUI countText;

    private int index;

    public void Init(int slotIndex, Action<int> onClick)
    {
        index = slotIndex;
        GetComponent<Button>().onClick.AddListener(() =>
        {
            onClick?.Invoke(index);
        });
    }

    public void Set(ItemInstance item, int count)
    {
        icon.sprite = item.Data.Icon;
        icon.enabled = true;

        countText.text = count > 1 ? count.ToString() : "";
    }

    public void Clear()
    {
        icon.sprite = null;
        icon.enabled = false;
        countText.text = "";
    }

}
