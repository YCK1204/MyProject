using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    public GameObject[] prefabs; // ������ ������
    private List<GameObject>[] pools; // ���� ������Ʈ ����Ʈ

    private void Awake()
    {
        InitializePools(); // Ǯ �ʱ�ȭ
    }

    // Ǯ �ʱ�ȭ �޼���
    private void InitializePools()
    {
        pools = new List<GameObject>[prefabs.Length];

        for (int i = 0; i < prefabs.Length; i++)
        {
            pools[i] = new List<GameObject>(); // �� ����Ʈ �ʱ�ȭ
        }
    }

    // ������Ʈ ��û �޼���
    public GameObject Get(int index)
    {
        if (index < 0 || index >= pools.Length)
        {
            Debug.LogError("Index " + index + " is out of bounds.");
            return null;
        }

        GameObject select = null;
        foreach (GameObject item in pools[index])
        {
            if (!item.activeSelf)
            {
                select = item;
                select.SetActive(true);
                break;
            }
        }

        if (!select)
        {
            select = Instantiate(prefabs[index], transform);
            pools[index].Add(select);
        }
        return select;
    }
}

