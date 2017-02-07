using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ItemModel{

    public static SortedDictionary<string,Item>  m_GridItem = new SortedDictionary<string, Item>();
    
    //以网格名称存储
    public static void StoreItem(string gridName, Item item)
    {
        if (m_GridItem.ContainsKey(gridName))
        {
            m_GridItem.Remove(gridName);
        }
           
        m_GridItem[gridName] = item;
    }

    public static Item GetItem(string gridName)
    {
        return m_GridItem.ContainsKey(gridName) ? m_GridItem[gridName] : null;
    }

    public static void DeleteItem(string gridName)
    {
        if (m_GridItem.ContainsKey(gridName))
            m_GridItem.Remove(gridName);
    }
}
