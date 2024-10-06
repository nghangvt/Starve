using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item Data", menuName = "Item Data", order = 50)]
public class ItemData : ScriptableObject
{
    public string itemName = "Item Name";
    public bool isStackable = false;
    public Sprite icon;
    public ItemType type;
}

public enum ItemType
{
    weapon,
    pickaxe,
    axe,
    spear,
    shield,
    food,
    Ore,
    Block,
    ItemCraft
}