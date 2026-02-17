using System;

public interface IInventoryView
{
    //View는 어떻게 보여줄지 만 책임
    //입력은 이벤트로 외부에 전달
    void CreateSlots(int Capacity);
    void SetSlot(int index, ItemInstance item);
    void ClearSlot(int index);

    event Action<int> OnSlotClicked;
}
