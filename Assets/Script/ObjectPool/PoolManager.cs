using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{




    public string poolName; // Ǯ�� �̸�
    public int initialSize; // �ʱ� Ǯ ũ��

    public GameObject[] prefabs; // ������ ������
    public List<GameObject>[] pools;
    // ���� ������Ʈ ����Ʈ




    private void Start()
    {
        AddI();
       
    }

    // Ǯ �ʱ�ȭ �޼���
    private void AddI()
    {

        pools = new List<GameObject>[prefabs.Length];
        for (int index = 0; index < pools.Length; index++)
            pools[index] = new List<GameObject>();
    }

    public GameObject Get(int index)
    {
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


       
    
