using System;
using System.Collections.Generic;
using UnityEngine;
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
            string[][] grid = CsvFileHandler.LoadTextFile(textAsset);
            for (int i = 1; i < grid.Length; i++)
            {
                ItemData row = new ItemData();
                row.globalID = Int32.Parse(grid[i][0]);
                row.categoryID = Int32.Parse(grid[i][1]);
                row.categoryName = grid[i][2];
                row.typeID = Int32.Parse(grid[i][3]);
                row.typeName = grid[i][4];
                typeNameList.Add(row.typeName);
                row.size = new IntVector2(Int32.Parse(grid[i][5]), Int32.Parse(grid[i][6]));
                row.icon = Resources.Load<Sprite>("itemIcons/" + grid[i][4]);
                dbList.Add(row);
            }
        }
        
        public void PassItemData(ref GameItem item)
        {
            int id = item.globalID;
            item.categoryID = dbList[id].categoryID;
            item.categoryName = dbList[id].categoryName;
            item.typeID = dbList[id].typeID;
            item.typeName = dbList[id].typeName;
            item.size = dbList[id].size;
            item.icon = dbList[id].icon;
        }

    }
}
