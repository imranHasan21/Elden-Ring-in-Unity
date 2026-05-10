using UnityEngine;

public class Item : ScriptableObject
{
    [Header("Item Information")]
    public string itemName;
    public Sprite itemIcon;
    public int itemID;
    [TextArea] public string itemDescription;
}
