using System;
using System.Collections.Generic;
using UnityEngine;

namespace InventoryGridSystem
{
    //IMPLEMENTAR!!!!!---
    public class ItemListManager : MonoBehaviour
    {
        //public ObjectPool itemButtonPool;
        //public ObjectPool itemEquipPool;
        //public InventoryGridManager inventoryGridManager;
        //public LoadItemDatabase itemDB;
        //public SortAndFilterManager sortManager;

        public float iconSize;
        
        //public List<ItemClass> startItemList;
        public List<GameObject> currentButtonList;
        //public  List<ItemClass> currentItemList;

        private Transform _contentPanel;
        private void Start()
        {
            _contentPanel = transform;
        }
    }
    
    
    
    
}
