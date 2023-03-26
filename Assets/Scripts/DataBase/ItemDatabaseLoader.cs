using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using Utility;

namespace DataBase
{
    public class ItemDatabaseLoader : MonoBehaviour
    {

        public TextAsset dbFile;

        public List<string> typeNameList = new List<string>();

        public List<ItemData> dbList = new List<ItemData>();

        private void Awake()
        {
            LoadDB(dbFile);
        }
        
        public class ItemData
        {
            public int globalID;
            public int categoryID;
            public string categoryName;
            public int typeID;
            public string typeName;
            public IntVector2 size;
            public Sprite icon;
        }
        
        private void LoadDB(TextAsset textAsset)
        {
            //string [][] grid =     
        }
    }
}
