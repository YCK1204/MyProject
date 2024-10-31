using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    public GameObject[] prefabs; // 관리할 프리팹
    private List<GameObject>[] pools; // 실제 오브젝트 리스트

    private void Awake()
    {
        InitializePools(); // 풀 초기화
    }

    // 풀 초기화 메서드
    private void InitializePools()
    {
        pools = new List<GameObject>[prefabs.Length];

        for (int i = 0; i < prefabs.Length; i++)
        {
            pools[i] = new List<GameObject>(); // 빈 리스트 초기화
        }
    }

    // 오브젝트 요청 메서드
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

