using System;
using System.Collections.Generic;
using UnityEngine;

namespace InventoryGridSystem
{
    
    public class ItemListManager : MonoBehaviour
    {
        public float iconSize;

        public List<GameObject> currentButtonList;

        private Transform _contentPanel;
        private void Start()
        {
            _contentPanel = transform;
        }
    }
    
    
    
    
}
