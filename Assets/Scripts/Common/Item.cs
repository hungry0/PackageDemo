using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[Serializable]
public class Item : IComparable<Item>{ 

    public int Id {get; private set; }

    public string Name { get; private set; }

    public string Description { get; private set; }

    public int BuyPrice { get; private set; }

    public int SellPrice { get; private set; }

    public string Icon { get; private set; }

    //Armor Consumable Weapon
    public string ItemType { get; protected set; }

    public Item()
    {
    }

    public Item(int id,string name,string description,int buyPrice,int sellPrice,string icon)
    {
        Id = id;
        Name = name;
        Description = description;
        BuyPrice = buyPrice;
        SellPrice = sellPrice;
        Icon = icon;
    }

    //排序，暂定规则是按类型首字母大小
    public int CompareTo(Item other)
    {
        if (this.ItemType[0].CompareTo(other.ItemType[0]) == 1)
            return 1;
        else if (this.ItemType[0].CompareTo(other.ItemType[0]) == -1)
            return -1;
        else if (this.ItemType[0].CompareTo(other.ItemType[0]) == 0)
        {
            return this.Name.CompareTo(other.Name);
        }
        else
        {
            return 0;
        }
    }
}
