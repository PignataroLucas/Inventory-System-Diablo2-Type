using Item;
using UnityEngine;
using UnityEngine.UI;
using Utility;

namespace InventoryGridSystem
{
    public class InventoryGridManager : MonoBehaviour
    {
        public GameObject[,] SlotGrid;
        public GameObject highlightedSlot;
        public Transform dropParent;

        [HideInInspector] public IntVector2 GridSize;
        
        public ItemListManager listManager;
        

        private IntVector2 _totalOffset, _checkSize, _checkStartPos;
        private IntVector2 _otherItemPos, _otherItemSize;

        private int _checkState;
        private bool _isOverEdge;
        
        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (highlightedSlot != null && InventoryItemInteraction.selectedItem != null && !_isOverEdge )
                {
                    switch (_checkState)
                    {
                        case 0 :
                            StoreItem(InventoryItemInteraction.selectedItem);
                            ColorChangeLoop(SlotColorHighlights.Blue, InventoryItemInteraction.selectedItemSize,
                                _totalOffset);
                            InventoryItemInteraction.ResetSelectedItem();
                            
                            break;
                        case 1 :
                            InventoryItemInteraction.SetSelectableItem(SwapItem(InventoryItemInteraction.selectedItem));
                            InventorySlotHighlighter.selector.PossOffset();
                            ColorChangeLoop(SlotColorHighlights.Gray , _otherItemSize , _otherItemPos);
                            RefreshColor(true);
                            
                            break;
                    }
                }
                else if (highlightedSlot != null  && InventoryItemInteraction.selectedItem == null && highlightedSlot.GetComponent<Slots>().isOccupied == true)
                {
                    ColorChangeLoop(SlotColorHighlights.Gray , highlightedSlot.GetComponent<Slots>().storedItemSize ,
                                                             highlightedSlot.GetComponent<Slots>().storedItemStartPos);
                    InventoryItemInteraction.SetSelectableItem(GetItem(highlightedSlot));
                    InventorySlotHighlighter.selector.PossOffset();
                    RefreshColor(true);
                }
            }
        }


        private void CheckArea(IntVector2 itemSize)
        {
            IntVector2 halfOffset;
            IntVector2 overCheck;

            halfOffset.x = (itemSize.x - (itemSize.x % 2 == 0 ? 0 : 1)) / 2;
            halfOffset.y = (itemSize.y - (itemSize.y % 2 == 0 ? 0 : 1)) / 2;
            
            _totalOffset = highlightedSlot.GetComponent<Slots>().GridPos - (halfOffset + InventorySlotHighlighter.posOffset);
            _checkStartPos = _totalOffset;
            _checkSize = itemSize;
            overCheck = _totalOffset + itemSize;
            _isOverEdge = false;

            if (overCheck.x > GridSize.x)
            {
                _checkSize.x = GridSize.x - _totalOffset.x;
                _isOverEdge = true;
            }
            if (_totalOffset.x < 0)
            {
                _checkSize.x = itemSize.x + _totalOffset.x;
                _checkStartPos.x = 0;
                _isOverEdge = true;
            }
            if (overCheck.y > GridSize.y)
            {
                _checkSize.y = GridSize.y - _totalOffset.y;
                _isOverEdge = true;
            }
            if (_totalOffset.y < 0)
            {
                _checkSize.y = itemSize.y + _totalOffset.y;
                _checkStartPos.y = 0;
                _isOverEdge = true;
            }
        }

        private int SlotCheck(IntVector2 itemSize)
        {
            GameObject obj = null;
            Slots slot;
            if (!_isOverEdge)
            {
                for (int y = 0; y < itemSize.y; y++)
                {
                    for (int x = 0; x < itemSize.x; x++)
                    {
                        slot = SlotGrid[_checkStartPos.x + x, _checkStartPos.y + y].GetComponent<Slots>();
                        if (slot.isOccupied)
                        {
                            if (obj == null)
                            {
                                obj = slot.storedItemObject;
                                _otherItemPos = slot.storedItemStartPos;
                                _otherItemSize = obj.GetComponent<InventoryItemInteraction>().item.size;
                            }
                            else if (obj != slot.storedItemObject) return 2;
                        }
                    }
                }
                if (obj == null) return 0;
                else return 1;
            }
            return 2;
        }

        public void RefreshColor(bool enter)
        {
            if (enter)
            {
                CheckArea(InventoryItemInteraction.selectedItemSize);
                _checkState = SlotCheck(_checkSize);
                switch (_checkState)
                {
                    case 0 : ColorChangeLoop(SlotColorHighlights.Green, _checkSize, _checkStartPos);break;
                    case 1 :
                        ColorChangeLoop(SlotColorHighlights.Yellow, _otherItemSize, _otherItemPos);
                        ColorChangeLoop(SlotColorHighlights.Green, _checkSize, _checkStartPos);
                        break;
                    case 2 : ColorChangeLoop(SlotColorHighlights.Red, _checkSize, _checkStartPos); break;
                }
            }
            else
            {
                _isOverEdge = false;
                //CheckArea(InventoryItemInteraction.selectedItemSize);
                SecondColorChangeLoop(_checkSize, _checkStartPos);
                if (_checkState == 1)
                {
                    ColorChangeLoop(SlotColorHighlights.Blue2, _otherItemSize, _otherItemPos);
                }
            }
        }
        public void ColorChangeLoop(Color32 color, IntVector2 size, IntVector2 startPos)
        {
            for (int y = 0;  y < size.y; y++)
            {
                for (int x = 0; x < size.x ; x++)
                {
                    SlotGrid[startPos.x + x, startPos.y + y].GetComponent<Image>().color = color;
                }
            }
        }
        private void SecondColorChangeLoop(IntVector2 size, IntVector2 startPos)
        {
            GameObject slot;
            for (int y = 0; y < size.y; y++)
            {
                for (int x = 0; x < size.x; x++)
                {
                    slot = SlotGrid[startPos.x + x, startPos.y + y];
                    if (slot.GetComponent<Slots>().isOccupied != false)
                    {
                        SlotGrid[startPos.x + x, startPos.y + y].GetComponent<Image>().color =
                            SlotColorHighlights.Blue2;
                    }
                    else
                    {
                        SlotGrid[startPos.x + x, startPos.y + y].GetComponent<Image>().color =
                            SlotColorHighlights.Gray;
                    }
                }
            }
        }

        private void StoreItem(GameObject item)
        {
            Slots slot;
            IntVector2 itemSize = item.GetComponent<InventoryItemInteraction>().item.size;
            for (int y = 0; y < itemSize.y; y++)
            {
                for (int x = 0; x < itemSize.x; x++)
                {
                    slot = SlotGrid[_totalOffset.x + x, _totalOffset.y + y].GetComponent<Slots>();
                    slot.storedItemObject = item;
                    slot.storedItemClass = item.GetComponent<InventoryItemInteraction>().item;
                    slot.storedItemSize = itemSize;
                    slot.storedItemStartPos = _totalOffset;
                    slot.isOccupied = true;
                    SlotGrid[_totalOffset.x + x, _totalOffset.y + y].GetComponent<Image>().color =
                        SlotColorHighlights.Gray;
                }
            }
            item.transform.SetParent(dropParent);
            item.GetComponent<RectTransform>().pivot = Vector2.zero;
            item.transform.position = SlotGrid[_totalOffset.x, _totalOffset.y].transform.position;
            item.GetComponent<CanvasGroup>().alpha = 1f;
            
        }
        private GameObject GetItem(GameObject slotObject)
        {
            Slots slot = slotObject.GetComponent<Slots>();
            GameObject item = slot.storedItemObject;
            IntVector2 tempItemPos = slot.storedItemStartPos;
            IntVector2 itemSize = item.GetComponent<InventoryItemInteraction>().item.size;

            Slots slotInstance;

            for (int y = 0; y < itemSize.y; y++)
            {
                for (int x = 0; x < itemSize.x; x++)
                {
                    slotInstance = SlotGrid[tempItemPos.x + x, tempItemPos.y + y].GetComponent<Slots>();
                    slotInstance.storedItemObject = null;
                    slotInstance.storedItemSize = IntVector2.Zero;
                    slotInstance.storedItemStartPos = IntVector2.Zero;
                    slotInstance.storedItemClass = null;
                    slotInstance.isOccupied = false;
                }
            }
            item.GetComponent<RectTransform>().pivot = new Vector2(.5f, .5f);
            item.GetComponent<CanvasGroup>().alpha = .5f;
            item.transform.position = Input.mousePosition;
            
            return item;
        }

        private GameObject SwapItem(GameObject item)
        {
            GameObject tempItem;
            tempItem = GetItem(SlotGrid[_otherItemPos.x, _otherItemPos.y]);
            StoreItem(item);
            return tempItem;
        }

       
        
    }
}

public struct SlotColorHighlights
{
    public static Color32 Green => new Color32(127, 223, 127, 255);

    public static Color32 Yellow => new Color32(223, 223, 63, 255);

    public static Color32 Red => new Color32(223, 127, 127, 255);

    public static Color32 Blue => new Color32(159, 159, 223, 255);

    public static Color32 Blue2 => new Color32(191, 191, 223, 255);

    public static Color32 Gray => new Color32(223, 223, 223, 255);
}
