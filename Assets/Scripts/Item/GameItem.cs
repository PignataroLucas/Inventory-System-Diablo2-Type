using System;
using DataBase;
using UnityEngine;
using Utility;

[System.Serializable]
public class GameItem
{

    public int globalID;
    public string typeName;
    
    [HideInInspector] public int categoryID;
    [HideInInspector] public int typeID;
    [HideInInspector] public string categoryName;
    [HideInInspector] public string serialID;
    [HideInInspector] public Sprite icon;
    [HideInInspector] public IntVector2 size;
    
    [Range(1, 100)] public int level;
    [Range(0, 4)] public int qualityInt;
    
    private enum Quality {Common, UnCommon, Rare, Epic, Legendary}

    public string GetQuality()
    {
        return Enum.GetName(typeof(Quality), qualityInt);
    }

    public static void SetItemValues(GameItem item, int id, int lvl, int quality)
    {
        item.globalID = id;
        item.level = lvl;
        item.qualityInt = quality;
        GameObject.FindGameObjectWithTag("ItemDatabase").GetComponent<ItemDatabaseLoader>().PassItemData(ref item);
    }

    public static void SetItemValues(GameItem item)
    {
        GameObject.FindGameObjectWithTag("ItemDatabase").GetComponent<ItemDatabaseLoader>().PassItemData(ref item);
    }

    public GameItem(GameItem passedItem)
    {
        globalID = passedItem.globalID;
        level = passedItem.level;
        qualityInt = passedItem.qualityInt;
    }
    
    public GameItem(){}
    
}
