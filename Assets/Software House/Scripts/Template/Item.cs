using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Software House/Item")]
public class Item : ScriptableObject
{
    public Sprite icon = null;
    public int itemID;
    public new string name = "New Item";

    public ItemType itemType;
    public string description;
    public GameObject itemPrefab;

}

public enum ItemType
{ None, Accessory, Booster, Stationary, Gadget }