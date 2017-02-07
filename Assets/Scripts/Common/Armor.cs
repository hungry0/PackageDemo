using UnityEngine;
using System.Collections;

public class Armor : Item {

    public int Power { get; private set; }

    public int Defend { get; private set; }

    public int Agility { get; private set; }

    public Armor() : base()
    {
        
    }

    public Armor(int id, string name, string description, int buyPrice, int sellPrice, string icon, int power, int defend, int agility) : base(id, name, description, buyPrice, sellPrice, icon)
    {
        Power = power;
        Defend = defend;
        Agility = agility;
        base.ItemType = "Armor";
    }
}
