using System;
using Item;
using UnityEngine;
using UnityEngine.EventSystems;
using Utility;

namespace InventoryGridSystem
{
    public class InventorySlotHighlighter : MonoBehaviour , IPointerEnterHandler , IPointerExitHandler
    {
        public GameObject slotParent;
        public static IntVector2 posOffset;
        public static InventorySlotHighlighter selector;
        //public static ItemOverlay overlay; ---IMPLEMENTAR!!!---
        public int quadNum;
        private InventoryGridManager _gridManager;
        private Slots _parentSlot;

        private void Start()
        {
            _gridManager = this.gameObject.transform.parent.parent.GetComponent<InventoryGridManager>();
            _parentSlot = slotParent.GetComponent<Slots>();
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            selector = this;
            _gridManager.highlightedSlot = slotParent;
            PossOffset();
            
            if (InventoryItemInteraction.selectedItem != null) _gridManager.RefreshColor(true);
            if (_parentSlot.storedItemObject != null && InventoryItemInteraction.selectedItem == null) 
                _gridManager.ColorChangeLoop(SlotColorHighlights.Blue , _parentSlot.storedItemSize,_parentSlot.storedItemStartPos);
            //if (_parentSlot.storedItemObject != null) overlay.UpdateOverlay(parentSlot.storedItem); ---IMPLEMENTAR!!!---
        }

        public void PossOffset()
        {
            if (InventoryItemInteraction.selectedItemSize.X != 0 &&
                InventoryItemInteraction.selectedItemSize.X % 2 == 0)
            {
                switch (quadNum)
                {
                    case 1:
                        posOffset.X = 0; break;
                    case 2:
                        posOffset.X = -1; break;
                    case 3:
                        posOffset.X = 0; break;
                    case 4:
                        posOffset.X = -1; break;
                }
            }
            if (InventoryItemInteraction.selectedItemSize.Y != 0 
                && InventoryItemInteraction.selectedItemSize.Y % 2 == 0)
            {
                switch (quadNum)
                {
                    case 1:
                        posOffset.Y = -1; break;
                    case 2:
                        posOffset.Y = -1; break;
                    case 3:
                        posOffset.Y = 0; break;
                    case 4:
                        posOffset.Y = 0; break;
                }
            }
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            selector = null;
            _gridManager.highlightedSlot = null;
            //overlay.UpdateOverlay(null); --IMPLEMENTAR--!!!!!!
            if(InventoryItemInteraction.selectedItem != null) _gridManager.RefreshColor(false);
            posOffset = IntVector2.Zero;
            if(_parentSlot.storedItemObject != null && InventoryItemInteraction.selectedItem == null)
                _gridManager.ColorChangeLoop(SlotColorHighlights.Blue2,_parentSlot.storedItemSize,_parentSlot.storedItemStartPos);
        }
        
    }
}
