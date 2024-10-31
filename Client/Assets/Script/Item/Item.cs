using UnityEngine;

public enum ItemType
{
    HealthPotion,
    ManaPotion,
    Weapon,
    Gate, // ����Ʈ Ÿ�� �߰�
    // �ٸ� ������ Ÿ�� �߰� ����
}

[System.Serializable]
public class Item
{
    public string itemName;
    public ItemType itemType;
    public int quantity;
    public Sprite icon; // ������ ������
    public GameObject gatePrefab; // ����Ʈ ������

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
                // ����Ʈ ������ ��� �� ����Ʈ ����
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
