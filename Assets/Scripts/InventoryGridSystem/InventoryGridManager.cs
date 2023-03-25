using System;
using System.Drawing;
using System.Net.Mime;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;
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
        public GameObject selectedButton;

        private IntVector2 _totalOffset, _checkSize, _checkStartPos;
        private IntVector2 _otherItemPos, _otherItemSize;

        private int _checkState;
        private bool _isOverEdge;
        
        //public ItemOverlay overlay;

        private void Start()
        {
            //ItemButton.iventoryManager = this;
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (highlightedSlot != null /*&& /*Item.selectedItem != null*/ && !_isOverEdge )
                {
                    switch (_checkState)
                    {
                        case 0 :
                            //StoreItem(Item.selectedItem);
                            //ColorChangeLoop(SlotColorHighlights.blue , Item.selectedItemSize , _totalOffset)
                            //Item.ResetSelectedItem();
                            //RemoveSelectedButton();
                        break;
                        case 1 :
                            //Item.SetSelectedItem(Swap(Item.selectedItem));
                            //SlotSelector.selector.PosOffset();
                            //ColorChangeLoop(SlotColorHighlights.Gray , _otherItemSize , _otherItemPos);
                            //RefreshColor(true);
                            //RemoveSelectedButton();
                            break;
                    }
                }
                else if (highlightedSlot != null /* && Item.selectedItem == null*/ && highlightedSlot.GetComponent<Slots>().isOccupied == true)
                {
                    //ColorChangeLoop(SlotColorHighlights.Gray , highlightedSlot.GetComponent<Slot>().storedItemSize ,
                    //                                         highlightedSlot.GetComponent<Slot>().storedItemStartPos);
                    //Item.SetSelectedItem(GetItem(highlightedSlot));
                    //SlotSelector.selector.PosOffset();
                    //RefreshColor(true);
                }
            }
        }


        private void CheckArea(IntVector2 itemSize)
        {
            IntVector2 halfOffset;
            IntVector2 overCheck;

            halfOffset.X = (itemSize.X - (itemSize.X % 2 == 0 ? 0 : 1)) / 2;
            halfOffset.Y = (itemSize.Y - (itemSize.Y % 2 == 0 ? 0 : 1)) / 2;
            
            _totalOffset = highlightedSlot.GetComponent<Slots>().GridPos - (halfOffset /* SlotSelector.posOffset*/);
            _checkStartPos = _totalOffset;
            _checkSize = itemSize;
            overCheck = _totalOffset + itemSize;
            _isOverEdge = false;

            if (overCheck.X > GridSize.X)
            {
                _checkSize.X = GridSize.X - _totalOffset.X;
                _isOverEdge = true;
            }
            if (_totalOffset.X < 0)
            {
                _checkSize.X = itemSize.X + _totalOffset.X;
                _checkStartPos.X = 0;
                _isOverEdge = true;
            }
            if (overCheck.Y > GridSize.Y)
            {
                _checkSize.Y = GridSize.Y - _totalOffset.Y;
                _isOverEdge = true;
            }
            if (_totalOffset.Y < 0)
            {
                _checkSize.Y = itemSize.Y + _totalOffset.Y;
                _checkStartPos.Y = 0;
                _isOverEdge = true;
            }
        }

        private int SlotCheck(IntVector2 itemSize)
        {
            GameObject obj = null;
            Slots slot;
            if (!_isOverEdge)
            {
                for (int y = 0; y < itemSize.Y; y++)
                {
                    for (int x = 0; x < itemSize.X; x++)
                    {
                        slot = SlotGrid[_checkStartPos.X + x, _checkStartPos.Y + y].GetComponent<Slots>();
                        if (slot.isOccupied)
                        {
                            if (obj == null)
                            {
                                obj = slot.storedItemObject;
                                _otherItemPos = slot.StoredItemStartPos;
                                //_otherItemSize = obj.GetComponent<Item>().item.Size;
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
                //CheckArea(Item.selectedItemSize);
                _checkState = SlotCheck(_checkSize);
                switch (_checkState)
                {
                    case 0 : ColorChangeLoop(SlotColorHighlights.Green, _checkSize, _checkStartPos);
                        break;
                    case 1 :
                        ColorChangeLoop(SlotColorHighlights.Yellow, _otherItemSize, _otherItemPos);
                        ColorChangeLoop(SlotColorHighlights.Green, _checkSize, _checkStartPos);
                        break;
                    case 2 : ColorChangeLoop(SlotColorHighlights.Red, _checkSize, _checkStartPos); 
                        break;
                }
            }
            else
            {
                _isOverEdge = false;
                //CheckArea();
                SecondColorChangeLoop(_checkSize, _checkStartPos);
                if (_checkState == 1)
                {
                    ColorChangeLoop(SlotColorHighlights.Blue2, _otherItemSize, _otherItemPos);
                }
            }
        }
        private void ColorChangeLoop(Color32 color, IntVector2 size, IntVector2 startPos)
        {
            for (int y = 0;  y < size.Y; y++)
            {
                for (int x = 0; x < size.X ; x++)
                {
                    SlotGrid[startPos.X + x, startPos.Y + y].GetComponent<Image>().color = color;
                }
            }
        }
        private void SecondColorChangeLoop(IntVector2 size, IntVector2 startPos)
        {
            GameObject slot;
            for (int y = 0; y < size.Y; y++)
            {
                for (int x = 0; x < size.X; x++)
                {
                    slot = SlotGrid[startPos.X + x, startPos.Y + y];
                    if (slot.GetComponent<Slots>().isOccupied != false)
                    {
                        SlotGrid[startPos.X + x, startPos.Y + y].GetComponent<Image>().color =
                            SlotColorHighlights.Blue2;
                    }
                    else
                    {
                        SlotGrid[startPos.X + x, startPos.Y + y].GetComponent<Image>().color =
                            SlotColorHighlights.Gray;
                    }
                }
            }
        }

        private void StoreItem(GameObject item)
        {
            
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
