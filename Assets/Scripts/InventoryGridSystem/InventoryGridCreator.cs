using System;
using InventoryGridSystem;
using UnityEngine;
using Utility;


    public class InventoryGridCreator : MonoBehaviour
    {
        public GameObject[,] slotGrid;
        public GameObject slotPrefab;
        public IntVector2 gridSize;
        public float slotSize;
        public float edgePadding;


        private void Awake()
        {
            slotGrid = new GameObject[gridSize.x, gridSize.y];
            ResizePanel();
            CreateSlots();
            GetComponent<InventoryGridManager>().GridSize = gridSize;
        }

        private void CreateSlots()
        {
            for (int y = 0; y < gridSize.y; y++)
            {
                for (int x = 0; x < gridSize.x; x++)
                {
                    GameObject obj = Instantiate(slotPrefab);
                    
                    obj.transform.name = "slot[" + x + "," + y + "]";
                    obj.transform.SetParent(transform);
                    RectTransform rectTransform = obj.transform.GetComponent<RectTransform>();
                    rectTransform.localPosition =
                        new Vector3(x * slotSize + edgePadding, y * slotSize + edgePadding, 0);
                    rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal,slotSize);
                    rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical,slotSize);
                    obj.GetComponent<RectTransform>().localScale = Vector3.one;
                    obj.GetComponent<Slots>().GridPos = new IntVector2(x, y);
                    slotGrid[x, y] = obj;
                }
            }
            GetComponent<InventoryGridManager>().SlotGrid = slotGrid;
        }

        private void ResizePanel()
        {
            float width, height;
            width = (gridSize.x * slotSize) + (edgePadding * 2);
            height = (gridSize.y * slotSize) + (edgePadding * 2);

            RectTransform rectTransform = GetComponent<RectTransform>();
            rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal,width);
            rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical,height);
            rectTransform.localScale = Vector3.one;
        }
        
    }
    

