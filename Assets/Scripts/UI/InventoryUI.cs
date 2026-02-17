using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour, IInventoryView
{
    [SerializeField] private InventorySlotUI slotPrefab;
    [SerializeField] private Transform slotParent;
    [SerializeField] private RectTransform content;

    private List<InventorySlotUI> slots = new();

    public event Action<int> OnSlotClicked;

    public void CreateSlots(int count)
    {
        for (int i = 0; i < count; i++)
        {
            var slot = Instantiate(slotPrefab, slotParent);
            slot.name = "Slot" + (i+1).ToString();
            slot.Init(i, HandleSlotClicked);
            slots.Add(slot);
        }
        StartCoroutine(UpdateGridColumnsNextFrame()); // 한 프레임 후 실행
    }

    private void HandleSlotClicked(int index)
    {
        OnSlotClicked?.Invoke(index);
    }

    public void ClearSlot(int index)
    {
        slots[index].Clear();
    }

    public void SetSlot(int index, ItemInstance item)
    {
        slots[index].Set(item, item.CurrentCount);
    }

    [SerializeField] private ScrollRect scrollRect;
    [SerializeField] private GridLayoutGroup gridLayoutGroup;
    [SerializeField] private float minCellSize = 100f; // 슬롯 Cell 크기 최소 100x100

    private void Awake()
    {
        gridLayoutGroup = content.GetComponent<GridLayoutGroup>();
    }

    private IEnumerator UpdateGridColumnsNextFrame()
    {
        yield return new WaitForEndOfFrame();

        Debug.Log($"viewport width: {scrollRect.viewport.rect.width}"); // 이제 정상 값
        UpdateGridColumns();
    }

    private void UpdateGridColumns()
    {
        float contentWidth = scrollRect.viewport.rect.width;                                //전체 content Size
        float totalPaddingX = gridLayoutGroup.padding.left + gridLayoutGroup.padding.right; //padding 값 계산 추가
        float spacingX = gridLayoutGroup.spacing.x;                                         //Slot spacing값 
        float availableWidth = contentWidth - totalPaddingX;                                // 전체 사이즈 - padding값 일단 빼고 가능한 size

        // cellSize 기준으로 한 줄에 들어갈 수 있는 최대 슬롯 수 계산
        // 슬롯이 정사각형(cellSize x cellSize)보다 작아지지 않도록
        // bestColumns - 최적의 개수 (전체사이즈 / 셀한개당 사이즈)
        int bestColumns = Mathf.Max(1, Mathf.FloorToInt((availableWidth + spacingX) / (minCellSize + spacingX)));

        // 남은 공간 없이 딱 맞게 cellWidth 계산
        float cellWidth = (availableWidth - spacingX * (bestColumns - 1)) / bestColumns;

        gridLayoutGroup.constraintCount = bestColumns;
        gridLayoutGroup.cellSize = new Vector2(cellWidth, cellWidth);   //정사각형으로 알맞게 설정

        LayoutRebuilder.ForceRebuildLayoutImmediate(gridLayoutGroup.GetComponent<RectTransform>());//레이아웃 갱신
    }
}
    
