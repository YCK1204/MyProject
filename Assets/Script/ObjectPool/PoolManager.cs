using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{




    public string poolName; // 풀의 이름
    public int initialSize; // 초기 풀 크기

    public GameObject[] prefabs; // 관리할 프리팹
    public List<GameObject>[] pools;
    // 실제 오브젝트 리스트




    private void Start()
    {
        AddI();
       
    }

    // 풀 초기화 메서드
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


       
    
