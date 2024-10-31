using UnityEngine;

public enum ItemType
{
    HealthPotion,
    ManaPotion,
    Weapon,
    Gate, // 게이트 타입 추가
    // 다른 아이템 타입 추가 가능
}

[System.Serializable]
public class Item
{
    public string itemName;
    public ItemType itemType;
    public int quantity;
    public Sprite icon; // 아이템 아이콘
    public GameObject gatePrefab; // 게이트 프리팹

    public Item(string name, ItemType type, int qty, Sprite itemIcon, GameObject gatePrefab = null)
    {
        itemName = name;
        itemType = type;
        quantity = qty;
        icon = itemIcon;
        this.gatePrefab = gatePrefab;
    }

    public void Use()
    {
        if (quantity > 0)
        {
            quantity--;
            if (gatePrefab != null)
            {
                // 게이트 아이템 사용 시 게이트 생성
                GameObject gateInstance = Object.Instantiate(gatePrefab);
                Debug.Log($"{itemName} used. A gate has been created.");
            }
            else
            {
                Debug.Log($"{itemName} used. Remaining: {quantity}");
            }
        }
        else
        {
            Debug.Log($"{itemName} is out of stock!");
        }
    }
}
