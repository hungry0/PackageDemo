using System;
using UnityEngine;
using System.Collections;

[Serializable]
public class Weapon : Item {

    public int Damage { get; private set; }

    public Weapon() : base()
    {
    }

    public Weapon(int id, string name, string description, int buyPrice, int sellPrice, string icon, int damage) : base(id, name, description, buyPrice, sellPrice, icon)
    {
        Damage = damage;
        base.ItemType = "Weapon";
    }

/*    public Weapon(int damage, int id, string name, string description, int buyPrice, int sellPrice, string icon,
        string itemType) : base(id, name, description, buyPrice, sellPrice, icon)
    {
        Damage = damage;
        base.ItemType = itemType;
    }  */
}
