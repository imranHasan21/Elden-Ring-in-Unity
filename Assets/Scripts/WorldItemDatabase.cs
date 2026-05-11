using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class WorldItemDatabase : MonoBehaviour
{
    public static WorldItemDatabase Instance;

    public WeaponItem unarmedWeapon;
    [SerializeField] List<WeaponItem> Weapons = new();
    private List<Item> items = new();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        // ADD ALL OF OUR WEAPONS TO THE LIST OF ITEMS
        foreach (var weapon in Weapons)
        {
            items.Add(weapon);
        }

        // ASSIGN ALL OF OUR ITEMS A UNIQUE ITEM ID
        for (int i = 0; i < items.Count; i++)
        {
            items[i].itemID = i;
        }
    }

    public WeaponItem GetWeaponByID(int ID)
    {
        // CHECK WEAPONS LIST AND RETURN THE MATCHED ID ACCORDING TO THE PASSED ID
        return Weapons.FirstOrDefault(weapon => weapon.itemID == ID);
    }
}
