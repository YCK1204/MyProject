using System.Collections.Generic;
using UnityEngine;


public class ItemManager : IManager
{
    private List<Item> items = new List<Item>();

    public void Init()
    {
        GameObject gatePrefab = Resources.Load<GameObject>("GatePrefab");
        // 초기 아이템 목록 설정

        items.Add(new Item("Health Potion", ItemType.HealthPotion, 5, null));
        items.Add(new Item("Mana Potion", ItemType.ManaPotion, 3, null));
        items.Add(new Item("Gate", ItemType.Weapon, 1, null, gatePrefab));
    }

    public void Clear()
    {
        items.Clear();
    }

    public void AddItem(Item item)
    {
        items.Add(item);
    }

    public Item GetItem(string name)
    {
        return items.Find(item => item.itemName == name);
    }

    public void UseItem(string name)
    {
        Item item = GetItem(name);
        if (item != null)
        {
            item.Use();
        }
        else
        {
      
        }

    }

}
