using UnityEngine;
using System.Collections;

public class Consumable : Item {

    public int Hp { get; private set; }
    public int Mp { get; private set; }

    public Consumable() : base()
    {
        
    }

    public Consumable(int id, string name, string description, int buyPrice, int sellPrice, string icon,int hp,int mp) : base(id, name, description, buyPrice, sellPrice, icon)
    {
        Hp = hp;
        Mp = mp;
        base.ItemType = "Consumable"; 
    }
}
