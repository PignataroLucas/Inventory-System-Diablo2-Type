using UnityEngine;
using UnityEngine.EventSystems;
using Utility;
using UnityEngine.UI;

namespace Item
{
    public class InventoryItemInteraction : MonoBehaviour , IPointerClickHandler
    {

        private GameObject _inventoryPanel;
        public static GameObject selectedItem;
        public static IntVector2 selectedItemSize;
        public static bool isDragging = false;

        private float _slotSize;
        
        public GameItem item;

        private void Awake()
        {
            //_slotSize = GameObject.FindGameObjectWithTag<"InventoryPanel">.GetComponent<InventoryGrid>().slotSize;
        }

        private void Update()
        {
            if (isDragging) selectedItem.transform.position = Input.mousePosition;
        }

        public void SetItemObject(GameItem passedItem)
        {
            RectTransform rectTransform = GetComponent<RectTransform>();
            rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal , passedItem.size.X * _slotSize);
            rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical , passedItem.size.Y * _slotSize);
            item = passedItem;
            GetComponent<Image>().sprite = passedItem.icon;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            
        }

        public static void SetSelectableItem(GameObject obj)
        {
            selectedItem = obj;
            selectedItemSize = obj.GetComponent<InventoryItemInteraction>().item.size;
            isDragging = true; 
            obj.transform.SetParent(GameObject.FindGameObjectWithTag("DragParent").transform);
            obj.GetComponent<RectTransform>().localScale = Vector3.one;
        }

        public static void ResetSelectedItem()
        {
            selectedItem = null;
            selectedItemSize = IntVector2.Zero;
            isDragging = false;
        }
        
        
    }
}